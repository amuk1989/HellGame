using System.Threading.Tasks;
using AR.Interfaces;
using Cysharp.Threading.Tasks;
using Scanning.Interfaces;
using UniRx;
using UnityEngine;
using Zenject;

namespace Scanning.Services
{
    internal class ScanningService: IScanningService
    {
        private readonly IARService _arService;
        private readonly IARProvider _arProvider;

        private readonly CompositeDisposable _compositeDisposable = new();

        public ScanningService(IARService arService, IARProvider arProvider)
        {
            _arService = arService;
            _arProvider = arProvider;
        }

        public async UniTask StartScanningTask()
        {
            _arService.ARInitialize();
            await UniTask.WaitUntil(() => _arService.IsInitialized);
            _arService.StartCollection();
        }

        public void StopScanning()
        {
            _arService.StopCollection();
            _compositeDisposable?.Clear();
        }
    }
}