using AR.Data;
using AR.Repositories;
using AR.View;
using JetBrains.Annotations;
using Niantic.ARDK.AR;
using Niantic.ARDK.AR.ARSessionEventArgs;
using Niantic.ARDK.AR.Mesh;
using UniRx;

namespace AR.Aggregates
{
    internal class ARAggregate
    {
        private readonly ARManager.ARFactory _arFactory;
        private readonly ARMeshManager.Factory _arMeshFactory;
        private readonly ARPlaneRepository _arPlaneRepository;
        
        private readonly ReactiveDictionary<string, PlaneData> _planeCollection = new();
        
        private ARManager _arManager;
        private ARMeshManager _arMeshManager;

        private ARAggregate(ARManager.ARFactory arFactory, ARMeshManager.Factory arMeshFactory)
        {
            _arFactory = arFactory;
            _arMeshFactory = arMeshFactory;
        }

        [CanBeNull] internal IARSession ARSession => _arManager.SessionManager.ARSession;
        [CanBeNull] internal IARMesh ARMesh => _arManager.SessionManager.ARSession.Mesh;

        internal void CreateARComponents()
        {
            _arManager ??= _arFactory.Create();
            _arMeshManager ??= _arMeshFactory.Create();
        }
    }
}