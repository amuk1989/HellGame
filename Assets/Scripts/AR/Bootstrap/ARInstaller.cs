using AR.Aggregates;
using AR.Data;
using AR.Interfaces;
using AR.Repositories;
using AR.Services;
using AR.View;
using Utility;
using Zenject;

namespace AR.Bootstrap
{
    public class ARInstaller: Installer
    {
        private ARDebugConfig _arDebugConfig;

        [Inject]
        private void Construct([InjectOptional]ARDebugConfig arDebugConfig)
        {
            _arDebugConfig = arDebugConfig;
        }
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<ARService>()
                .AsSingle()
                .NonLazy();

            Container
                .BindFactory<ARManager, ARManager.ARFactory>()
                .FromComponentInNewPrefabResource(Consts.ARManager);
            
            Container
                .BindFactory<ARMeshManager, ARMeshManager.Factory>()
                .FromComponentInNewPrefabResource(Consts.ARMesh);

            Container
                .BindInterfacesAndSelfTo<ARAggregate>()
                .AsSingle()
                .NonLazy();

            Container
                .Bind<ARPlaneRepository>()
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<ARMeshRepository>()
                .AsSingle()
                .NonLazy();
            
            if (_arDebugConfig == null) return;

            Container
                .BindInterfacesTo<ARDebugger>()
                .AsSingle()
                .NonLazy();

            Container
                .BindFactory<AnchorView, PlaceholderFactory<AnchorView>>()
                .FromComponentInNewPrefab(_arDebugConfig.AnchorView);
        }
    }
}