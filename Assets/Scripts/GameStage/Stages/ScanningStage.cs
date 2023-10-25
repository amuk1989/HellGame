using GameStage.Interfaces;
using PlaneMeshing.Interfaces;
using Scanning.Interfaces;

namespace GameStage.Stages
{
    internal class ScanningStage: IGameStage
    {
        private readonly IScanningService _scanningService;
        private readonly IPlaneRecognizer _planeRecognizer;
        private readonly IPortalTextureProvider _portalTextureProvider;

        public ScanningStage(IScanningService scanningService, IPlaneRecognizer planeRecognizer, 
            IPortalTextureProvider portalTextureProvider)
        {
            _scanningService = scanningService;
            _planeRecognizer = planeRecognizer;
            _portalTextureProvider = portalTextureProvider;
        }

        public async void Execute()
        {
            _planeRecognizer.StartRecognizer();
            await _scanningService.StartScanningTask();
            _portalTextureProvider.CreateTexture();
        }

        public void Complete()
        {
            _planeRecognizer.StopRecognizer();
            _scanningService.StopScanning();
        }
    }
}