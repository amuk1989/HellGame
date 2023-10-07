using AR.View;

namespace AR.Aggregates
{
    internal class ARAggregate
    {
        private readonly ARManager.ARFactory _arFactory;
        private readonly ARMeshManager.Factory _arMeshFactory;
        
        private ARManager _arManager;
        private ARMeshManager _arMeshManager;

        private ARAggregate(ARManager.ARFactory arFactory, ARMeshManager.Factory arMeshFactory)
        {
            _arFactory = arFactory;
            _arMeshFactory = arMeshFactory;
        }

        internal void CreateARComponents()
        {
            _arManager ??= _arFactory.Create();
            _arMeshManager ??= _arMeshFactory.Create();
        }
    }
}