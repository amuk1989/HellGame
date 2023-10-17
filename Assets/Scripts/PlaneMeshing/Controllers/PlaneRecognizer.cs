using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AR.Data;
using AR.Interfaces;
using ModestTree;
using PlaneMeshing.Interfaces;
using PlaneMeshing.Jobs;
using PlaneMeshing.Repositories;
using PlaneMeshing.Utilities;
using PlaneMeshing.View;
using UniRx;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

namespace PlaneMeshing.Aggregates
{
    public class PlaneRecognizer: IInitializable, IPlaneRecognizer
    {
        private readonly CompositeDisposable _compositeDisposable = new();

        private readonly IARProvider _arProvider;
        private readonly PlaneMeshRepository _planeMeshRepository;

        private readonly Semaphore _semaphore = new Semaphore(1,1);
        private JobHandle _jobHandle;

        private PlaneRecognizer(IARProvider arProvider, PlaneMeshRepository planeMeshRepository)
        {
            _arProvider = arProvider;
            _planeMeshRepository = planeMeshRepository;
        }

        public void Initialize()
        {
            StartRecognizer();
        }

        public void StartRecognizer()
        {
            _arProvider
                .OnMeshUpdated
                .Subscribe(meshData =>
                {
                    _semaphore.WaitOne();
                    
                    Recognize(meshData);

                    _semaphore.Release();
                })
                .AddTo(_compositeDisposable);

            _arProvider
                .OnMeshRemoved
                .Subscribe(meshData => _planeMeshRepository.RemovePlane(meshData.Id))
                .AddTo(_compositeDisposable);
        }

        private void Recognize(UpdatedMeshData meshData)
        {
            var planes = _arProvider.Planes;

            for (int i = 0; i < planes.Count; i++)
            {
                var originVertices = new NativeArray<Vector3>(meshData.Mesh.vertices, Allocator.TempJob);
                var originTriangles = new NativeArray<int>(meshData.Mesh.triangles, Allocator.TempJob);
                var triangles = new NativeArray<bool>(meshData.Mesh.triangles.Length, Allocator.TempJob);

                var job = new SearchTrianglesJob()
                {
                    Vertices = originVertices,
                    Triangles = originTriangles,
                    ValidateTriangles = triangles,
                    AreaBounce = planes[i].Extends,
                    AreaCenter = planes[i].Center,
                    PlaneRotation = planes[i].Rotation,
                    PlaneOrientations = planes[i].PlaneOrientation
                };

                var handle = job.Schedule(meshData.Mesh.triangles.Length, 3);
                handle.Complete();

                var antiAliasingJob = new AntialiasingPlaneJob()
                {
                    PlaneRotation = planes[i].Rotation,
#if UNITY_EDITOR
                    PlaneYPosition = -planes[i].Center.y,
                    #else
                    PlaneYPosition = planes[i].Center.y,
#endif

                    Vertices = originVertices
                };

                var antiAliasingHandle = antiAliasingJob.Schedule(originVertices.Length, 64);
                antiAliasingHandle.Complete();

                var validCount = triangles.Count(x => x);
                    
                if (validCount == 0) continue;

                var validTriangles = MeshingUtility.GetValidVertices(originTriangles, triangles, validCount);

                var data = GetMeshData(validTriangles, new NativeArray<Vector3>(meshData.Mesh.vertices, Allocator.TempJob));

                var planeMesh = CreateMesh(data);

                _planeMeshRepository.AddPlane(meshData.Id, planeMesh);
            }
        }

        private static Mesh.MeshDataArray GetMeshData(NativeArray<int> triangles, NativeArray<Vector3> vertices)
        {
            var dataArray = Mesh.AllocateWritableMeshData(1);
            var data = dataArray[0];
            
            data.SetVertexBufferParams(vertices.Length,
                new VertexAttributeDescriptor(VertexAttribute.Position),
                new VertexAttributeDescriptor(VertexAttribute.Normal, stream: 1));
            
            var pos = data.GetVertexData<Vector3>();

            for (int i = 0; i < pos.Length; i++)
            {
                pos[i] = vertices[i];
            }
            
            data.SetIndexBufferParams(triangles.Length, IndexFormat.UInt16);
            var ib = data.GetIndexData<ushort>();
            
            for (ushort i = 0; i < ib.Length; ++i)
                ib[i] = (ushort)triangles[i];
            
            data.subMeshCount = 1;
            
            data.SetSubMesh(0, new SubMeshDescriptor(0, ib.Length));

            return dataArray;
        }

        private Mesh CreateMesh(Mesh.MeshDataArray dataArray)
        {
            var mesh = new Mesh();
            
            Mesh.ApplyAndDisposeWritableMeshData(dataArray, mesh);
            
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();

            return mesh;
        }

        public void StopRecognizer()
        {
            _compositeDisposable.Clear();
        }
    }
}