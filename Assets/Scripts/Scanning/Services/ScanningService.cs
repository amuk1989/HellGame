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

        public async UniTask AsyncScanningTask()
        {
            _arService.ARInitialize();
            await UniTask.WaitUntil(() => _arService.IsInitialized);
            _arService.StartCollection();
            
            SubscribeToAR();
        }

        public void StopScanning()
        {
            _arService.StopCollection();
            _compositeDisposable?.Clear();
        }

        private void SubscribeToAR()
        {
            // _arProvider.OnMeshUpdated
            //     .Subscribe(_ => Debug.Log("Updated meshes"))
            //     .AddTo(_compositeDisposable);
            //
            // _arProvider.OnPlaneUpdated
            //     .Subscribe(plane => Debug.Log($"Updated plane {plane.Center}"))
            //     .AddTo(_compositeDisposable);
        }
    }
}