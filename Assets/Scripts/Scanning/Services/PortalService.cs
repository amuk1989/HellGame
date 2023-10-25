using AR.Interfaces;
using Cysharp.Threading.Tasks;
using Niantic.ARDK.AR;
using Scanning.Interfaces;
using UnityEngine;
using Zenject;

namespace Scanning.Services
{
    public class PortalService: IInitializable, IPortalTextureProvider
    {
        private readonly ICameraProvider _cameraProvider;
        public RenderTexture Texture { get; private set; }

        public PortalService(ICameraProvider cameraProvider)
        {
            _cameraProvider = cameraProvider;
        }

        public void Initialize()
        {
            
        }

        public void CreateTexture()
        {
            Texture = new RenderTexture(Screen.width, Screen.height, 24);
            _cameraProvider.PortalCamera.targetTexture = Texture;
            RenderTexture.active = Texture;
        }
    }
}