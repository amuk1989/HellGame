﻿using AR.Repositories;
using AR.View;
using JetBrains.Annotations;
using Niantic.ARDK.AR;
using Niantic.ARDK.AR.Configuration;
using Niantic.ARDK.AR.Mesh;
using UnityEngine;
using Zenject;

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

        [CanBeNull] internal IARSession ARSession => _arManager?.SessionManager.ARSession;
        [CanBeNull] internal IARMesh ARMesh => _arManager?.SessionManager.ARSession.Mesh;
        [CanBeNull] internal Camera ARCamera => _arManager?.ARCamera;

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