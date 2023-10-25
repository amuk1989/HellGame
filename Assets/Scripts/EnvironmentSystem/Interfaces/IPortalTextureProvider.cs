using UnityEngine;

namespace EnvironmentSystem.Interfaces
{
    public interface IPortalTextureProvider
    {
        public RenderTexture Texture { get; }
        public void CreateTexture();
    }
}