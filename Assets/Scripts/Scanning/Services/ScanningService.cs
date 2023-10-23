using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AR.Interfaces;
using Cysharp.Threading.Tasks;
using PlaneMeshing.Interfaces;
using Scanning.Data;
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
        private readonly RoomConfigData _roomConfig;

        private readonly CompositeDisposable _compositeDisposable = new();
        private readonly ReactiveProperty<float> _scannedArea = new();
        private readonly ReactiveCommand _onEnoughScanned = new();

        private readonly Stopwatch _stopwatch = new();

        private bool _isEnoughScanned = false;

        public ScanningService(IARService arService, IARProvider arProvider, IPlaneMeshesProvider planeMeshesProvider,
            RoomConfigData roomConfig)
        {
            _arService = arService;
            _arProvider = arProvider;
            _planeMeshes = planeMeshesProvider;
            _roomConfig = roomConfig;
        }

        public IObservable<float> ScannedAreaAsObservable() => _scannedArea.AsObservable();
        public IObservable<Unit> ScannedEnoughArea() => _onEnoughScanned.AsObservable();

        public async UniTask StartScanningTask()
        {
            _arService.ARInitialize();
            await UniTask.WaitUntil(() => _arService.IsInitialized);
            _arService.StartCollection();
            
            StartCalculateArea();
        }

        private void StartCalculateArea()
        {
            Observable
                .Timer(TimeSpan.FromSeconds(1))
                .Repeat()
                .Subscribe(data =>
                {
                    _stopwatch.Restart();
                    var scannedArea = 0f;
                    foreach (var mesh in _planeMeshes.PlaneMeshes)
                    {
                        scannedArea += AreaCalculationUtility.CalculatePlaneArea(mesh.Value);
                    }

                    _scannedArea.Value = scannedArea;
                    
                    _stopwatch.Stop();
                    UnityEngine.Debug.Log($"[ScanningService] Ticks {_stopwatch.ElapsedTicks}{Environment.NewLine}" +
                                          $"ms {_stopwatch.ElapsedMilliseconds}");
                    
                    if (_isEnoughScanned || _scannedArea.Value < _roomConfig.MinArea) return;

                    _isEnoughScanned = true;
                    _onEnoughScanned.Execute();
                })
                .AddTo(_compositeDisposable);
        }

        public void StopScanning()
        {
            _arService.StopCollection();
            _compositeDisposable?.Clear();
            _isEnoughScanned = false;
            _scannedArea.Value = 0;
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
    }
}