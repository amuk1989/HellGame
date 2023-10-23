using System;
using Base.Data;
using Base.Interfaces;
using Debuger.View;
using UnityEngine;

namespace Debuger.Data
{
    [CreateAssetMenu(fileName = "DebugConfig", menuName = "Debug configs/DebugConfig", order = 0)]
    public class DebugConfig : ScriptableObject
    {
        [SerializeField] private bool _isShownFPS;
        [SerializeField] private FPSViewer _fpsViewer;
        
        public bool IsShownFPS => _isShownFPS;
        public FPSViewer Viewer => _fpsViewer;
    }
}