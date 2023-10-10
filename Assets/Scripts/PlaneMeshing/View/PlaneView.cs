using System;
using UnityEngine;
using Zenject;

namespace PlaneMeshing.View
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class PlaneView: MonoBehaviour
    {
        [SerializeField] private MeshFilter _meshFilter;

        private Mesh _mesh;

        [Inject]
        private void Construct(Mesh mesh)
        {
            _mesh = mesh;
        }

        private void Start()
        {
            _meshFilter.mesh = _mesh;
            gameObject.name = _mesh.name;
        }
    }
}