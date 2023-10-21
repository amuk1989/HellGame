using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AR.Interfaces;
using Cysharp.Threading.Tasks;
using PlaneMeshing.Interfaces;
using Scanning.Interfaces;
using Scanning.Utilities;
using UniRx;

namespace Scanning.Services
{
    internal class ScanningService: IScanningService, IDisposable
    {
        private readonly IARService _arService;
        private readonly IARProvider _arProvider;
        private readonly IPlaneMeshesProvider _planeMeshes;

        private readonly CompositeDisposable _compositeDisposable = new();
        private volatile float _scannedArea;

        private readonly Stopwatch _stopwatch = new();

        public ScanningService(IARService arService, IARProvider arProvider, IPlaneMeshesProvider planeMeshesProvider)
        {
            _arService = arService;
            _arProvider = arProvider;
            _planeMeshes = planeMeshesProvider;
        }

        public float ScannedArea => _scannedArea;

        public async UniTask StartScanningTask()
        {
            _arService.ARInitialize();
            await UniTask.WaitUntil(() => _arService.IsInitialized);
            _arService.StartCollection();
            
            StartCalculateArea();
        }

        private void StartCalculateArea()
        {
            _planeMeshes
                .PlaneMeshUpdateAsObservable()
                .Subscribe(data =>
                {
                    _stopwatch.Reset();
                    _scannedArea += AreaCalculationUtility.CalculatePlaneArea(data.Value);
                    _stopwatch.Stop();
                    UnityEngine.Debug.Log($"[ScanningService] Ticks {_stopwatch.ElapsedTicks}{Environment.NewLine}" +
                                          $"ms {_stopwatch.ElapsedMilliseconds}");
                })
                .AddTo(_compositeDisposable);
            
            _planeMeshes
                .PlaneMeshRemoveAsObservable()
                .Subscribe(data =>
                {
                    _stopwatch.Reset();
                    _scannedArea -= AreaCalculationUtility.CalculatePlaneArea(data.Value);
                    _stopwatch.Stop();
                    UnityEngine.Debug.Log($"[ScanningService] Ticks {_stopwatch.ElapsedTicks}{Environment.NewLine}" +
                                          $"ms {_stopwatch.ElapsedMilliseconds}");
                })
                .AddTo(_compositeDisposable);
        }

        public void StopScanning()
        {
            _arService.StopCollection();
            _compositeDisposable?.Clear();
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
    }
}