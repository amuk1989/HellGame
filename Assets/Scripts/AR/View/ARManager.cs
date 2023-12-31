﻿using Niantic.ARDK.Extensions;
using UnityEngine;
using Zenject;

namespace AR.View
{
    internal class ARManager : MonoBehaviour
    {
        internal class ARFactory: PlaceholderFactory<ARManager>
        {
        }
        
        [SerializeField] private ARSessionManager _sessionManager;
        [SerializeField] private Camera _arCamera;
        [SerializeField] private Camera _portalCamera;
            
        public ARSessionManager SessionManager => _sessionManager;
        public Camera ARCamera => _arCamera;
        public Camera PortalCamera => _portalCamera;
    }
}