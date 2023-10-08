using System.Threading.Tasks;
using AR.Interfaces;
using Cysharp.Threading.Tasks;
using Scanning.Interfaces;
using Zenject;

namespace Scanning.Services
{
    internal class ScanningService: IScanningService
    {
        private readonly IARService _arService;

        public ScanningService(IARService arService)
        {
            _arService = arService;
        }

        public async UniTask AsyncScanningTask()
        {
            _arService.ARInitialize();
            await UniTask.WaitUntil(() => _arService.IsInitialized);
            _arService.StartCollection();
        }
    }
}