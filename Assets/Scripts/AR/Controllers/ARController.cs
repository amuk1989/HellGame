using AR.Repositories;
using AR.View;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Zenject;
using ARMeshManager = AR.View.ARMeshManager;

namespace AR.Controllers
{
    internal class ARController: IInitializable
    {
        private readonly ARManager.ARFactory _arFactory;
        private readonly ARMeshManager.Factory _arMeshFactory;
        private readonly ARPlaneRepository _arPlaneRepository;
        
        private IARWorldTrackingConfiguration _worldTrackingConfiguration;
        private ARManager _arManager;
        private ARMeshManager _arMeshManager;
        private IARSession _session;

        private ARController(ARManager.ARFactory arFactory, ARMeshManager.Factory arMeshFactory)
        {
            _arFactory = arFactory;
            _arMeshFactory = arMeshFactory;
        }

        [CanBeNull] internal ARSession ARSession => _arManager?.SessionManager.ARSession;
        [CanBeNull] internal A ARMesh => _arManager?.SessionManager.ARSession.Mesh;
        [CanBeNull] internal Camera ARCamera => _arManager?.ARCamera;
        [CanBeNull] internal Camera PortalCamera => _arManager?.PortalCamera;

        public void Initialize()
        {
        }

        internal void CreateARComponents()
        {
            _arManager ??= _arFactory.Create();
            _arMeshManager ??= _arMeshFactory.Create();

            _session = _arManager.SessionManager.ARSession;
        }

        internal void StopMeshing()
        {
            // _arMeshManager.Dispose();
        }
    }
}