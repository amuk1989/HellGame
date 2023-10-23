using Debuger.Data;
using Debuger.View;
using UnityEngine;
using Zenject;

namespace Debuger.Bootstrap
{
    public class DebugInstaller: Installer
    {
        [InjectOptional] private DebugConfig _debugConfig;
        
        public override void InstallBindings()
        {
            if (_debugConfig == null) return;

            if (_debugConfig.IsShownFPS) Container.Bind<FPSViewer>().FromComponentInNewPrefab(_debugConfig.Viewer).AsSingle().NonLazy();
        }
    }
}