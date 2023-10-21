using System;
using Cysharp.Threading.Tasks;
using UniRx;
using Zenject;

namespace Scanning.Interfaces
{
    public interface IScanningService
    {
        public UniTask StartScanningTask();
        public void StopScanning();
        public IObservable<float> ScannedAreaAsObservable();
        public IObservable<Unit> ScannedEnoughArea();
    }
}