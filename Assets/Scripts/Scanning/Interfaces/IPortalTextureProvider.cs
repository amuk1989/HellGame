using UnityEngine;

namespace Scanning.Interfaces
{
    public interface IPortalTextureProvider
    {
        public RenderTexture Texture { get; }
        public void CreateTexture();
    }
}