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
                        var triangles = MeshingUtility.GetInsideVertices(meshes[i], plane.Center, plane.Extends);
                        if (triangles.Item1 == 0) continue;
                        Debug.Log(triangles.Item1);
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

            var plane = planes.First();
                    
            for (int i = 0; i < meshes.Length; i++)
            {
                var trianglesData = MeshingUtility.GetInsideVertices(meshes[i], plane.Center, plane.Extends);
                if (trianglesData.Item1 == 0) continue;
                Debug.Log(trianglesData.Item1);
                DeleteTrianglesAndVertices(meshes[i], trianglesData.Item2, trianglesData.Item1);
            }
        }

        private void DeleteTrianglesAndVertices(Mesh mesh, NativeArray<bool> isValidTriangles, int size)
        {
            var triangles = new List<int>(mesh.triangles);

            for (int i = 0; i < triangles.Count; i++)
            {
                if (isValidTriangles[i]) continue; 
                triangles[i] = -1;
            }

            triangles.RemoveAll(x => x == -1);

            var trianglesArray = triangles.ToArray();

            var newMesh = new Mesh();
            
            newMesh.vertices = mesh.vertices;
            newMesh.uv = mesh.uv;
            newMesh.normals = mesh.normals;
            newMesh.triangles = trianglesArray;
            
            newMesh.RecalculateBounds();
            newMesh.RecalculateNormals();

            _planeFactory.Create(newMesh);
        }

        public void StopRecognizer()
        {
            _compositeDisposable.Clear();
        }
    }
}