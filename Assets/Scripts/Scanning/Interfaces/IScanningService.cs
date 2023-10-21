using Cysharp.Threading.Tasks;
using Zenject;

namespace Scanning.Interfaces
{
    public interface IScanningService
    {
        public UniTask StartScanningTask();
        public void StopScanning();
    }
}