using UnityEngine;
using Zenject;

namespace EnvironmentSystem.View
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class HoleView : MonoBehaviour
    {
        public class Factory: PlaceholderFactory<RenderTexture, Material, Mesh, HoleView>
        {
        }

        [SerializeField] private Renderer _renderer;
        [SerializeField] private MeshFilter _meshFilter;
        
        private static readonly int BaseMap = Shader.PropertyToID("_BaseMap");

        [Inject]
        private void Construct(RenderTexture texture, Material material, Mesh mesh)
        {
            _renderer.material = material;
            _meshFilter.mesh = mesh;
            _renderer.material.SetTexture(BaseMap, texture);
            
#if UNITY_EDITOR
            transform.localScale = new Vector3(1, -1, 1);
#endif
        }
    }
}