using Zenject;

namespace AR.Interfaces
{
    public interface IARService
    {
        public void ARInitialize();
        public void StartCollection();
        public void StopCollection();
        public bool IsInitialized { get; }
    }
}