using System;
using UnityEngine;
using Zenject;

namespace PlaneMeshing.View
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class PlaneView: MonoBehaviour, IDisposable
    {
        public class Factory: PlaceholderFactory<LayerMask, Material, Mesh, PlaneView>
        {
        }
        
        [SerializeField] private MeshFilter _meshFilter;
        [SerializeField] private MeshRenderer _meshRenderer;

        private Mesh _mesh;
        private Material _material;
        private LayerMask _mask;

        [Inject]
        private void Construct(LayerMask mask, Mesh mesh, Material material)
        {
            _material = material;
            _mesh = mesh;
            _mask = mask;
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

            gameObject.layer = (int)Mathf.Log(_mask+1,2);;
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}