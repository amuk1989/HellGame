using System;
using UnityEngine;
using Zenject;

namespace PlaneMeshing.View
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class PlaneView: MonoBehaviour, IDisposable
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
#if UNITY_EDITOR
            transform.localScale = new Vector3(1, -1, 1);
            #else
            transform.localScale = new Vector3(1, 1, 1);
#endif
            _meshFilter.mesh = _mesh;
            gameObject.name = _mesh.name;
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}