using System;
using UnityEngine;
using Zenject;

namespace PlaneMeshing.View
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class PlaneView: MonoBehaviour, IDisposable
    {
        [SerializeField] private MeshFilter _meshFilter;
        [SerializeField] private MeshRenderer _meshRenderer;

        private Mesh _mesh;
        private Material _material;

        [Inject]
        private void Construct(Mesh mesh, Material material)
        {
            _material = material;
            _mesh = mesh;
        }

        private void Start()
        {
#if UNITY_EDITOR
            transform.localScale = new Vector3(1, -1, 1);
            #else
            transform.localScale = new Vector3(1, 1, 1);
#endif
            _meshFilter.mesh = _mesh;
            _meshRenderer.material = _material;
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}