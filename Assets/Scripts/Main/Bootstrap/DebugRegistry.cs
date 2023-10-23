using AR.Data;
using Debuger.Data;
using UnityEngine;
using Zenject;

namespace Main.Bootstrap
{
    [CreateAssetMenu(fileName = "DebugRegistry", menuName = "Registries/DebugRegistry")]
    public class DebugRegistry: ScriptableObjectInstaller
    {
        [SerializeField] private bool _isEnable;
        [SerializeField] private ARDebugConfig _arDebug;
        [SerializeField] private DebugConfig _debug;

        public override void InstallBindings()
        {
            if (!_isEnable) return;
            
            InstallDebugRegistry(_arDebug);
            InstallDebugRegistry(_debug);
        }
        
        private void InstallDebugRegistry<TRegistry>(TRegistry registry) where TRegistry:ScriptableObject
        {
            if (registry == null) return;
            
            Container
                .Bind<TRegistry>()
                .FromInstance(registry);
        }
    }
}