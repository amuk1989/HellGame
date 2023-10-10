using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AR.Interfaces;
using ModestTree;
using PlaneMeshing.Utilities;
using PlaneMeshing.View;
using UniRx;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

namespace PlaneMeshing.Aggregates
{
    public class PlaneRecognizer: IInitializable
    {
        private readonly CompositeDisposable _compositeDisposable = new();

        private readonly IARProvider _arProvider;
        private readonly PlaceholderFactory<Mesh, PlaneView> _planeFactory;

        private readonly Semaphore _semaphore = new Semaphore(1,1);

        public PlaneRecognizer(IARProvider arProvider, PlaceholderFactory<Mesh, PlaneView> planeFactory)
        {
            _arProvider = arProvider;
            _planeFactory = planeFactory;
        }

        public void Initialize()
        {
            // StartRecognizer();
        }

        public void StartRecognizer()
        {
            _arProvider
                .OnMeshUpdated
                .Subscribe(_ =>
                {
                    _semaphore.WaitOne();
                    var meshes = _arProvider.Meshes.ToArray();
                    var positions = _arProvider.MeshPositions.ToArray();
                    var planes = _arProvider.Planes;
                    
                    if (meshes.Length == 0 || planes.IsEmpty()) return;

                    var plane = planes.First();
                    
                    for (int i = 0; i < meshes.Length; i++)
                    {
                        // var triangles = MeshingUtility.CheckInsideVertices(meshes[i], plane.Center, plane.Extends);
                        // if (triangles.Item1 == 0) continue;
                        // Debug.Log(triangles.Item1);
                        // CreatePlaneMesh(meshes[i].vertices, meshes[i].triangles );
                        // DeleteTriangles(meshes[i], triangles.Item2.ToArray());
                        // break;
                    }

                    _semaphore.Release();
                })
                .AddTo(_compositeDisposable);
        }

        public void Recognize()
        {
            var meshes = _arProvider.Meshes.ToArray();
            var planes = _arProvider.Planes;
                    
            if (meshes.Length == 0 || planes.IsEmpty()) return;

            for (int i = 0; i < planes.Count; i++)
            {
                for (int j = 0; j < meshes.Length; j++)
                {
                    var triangles = MeshingUtility.GetInsideVertices(meshes[j], planes[i].Center, planes[i].Extends);
                    if (triangles.Length == 0) continue;
                    
                    var data = GetMeshData(Mesh.AllocateWritableMeshData(1), triangles, new NativeArray<Vector3>(meshes[j].vertices, Allocator.TempJob));
                    
                    CreateMesh(data);
                }
            }
        }

        private static Mesh.MeshDataArray GetMeshData(Mesh.MeshDataArray dataArray, NativeArray<int> triangles, NativeArray<Vector3> vertices)
        {
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

        private void CreateMesh(Mesh.MeshDataArray dataArray)
        {
            var mesh = new Mesh();
            
            Mesh.ApplyAndDisposeWritableMeshData(dataArray, mesh);
            
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();

            _planeFactory.Create(mesh);
        }

        public void StopRecognizer()
        {
            _compositeDisposable.Clear();
        }
    }
}