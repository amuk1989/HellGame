using AR.Aggregates;
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
        public override void InstallBindings()
        {
#if UNITY_EDITOR
            Container
                .BindInterfacesTo<MockARService>()
                .AsSingle()
                .NonLazy();
#else
            Container
                .BindInterfacesTo<ARService>()
                .AsSingle()
                .NonLazy();
#endif

            Container
                .BindFactory<ARManager, ARManager.ARFactory>()
                .FromComponentInNewPrefabResource(Consts.ARManager);
            
            Container
                .BindFactory<ARMeshManager, ARMeshManager.Factory>()
                .FromComponentInNewPrefabResource(Consts.ARMesh);
            
#if UNITY_EDITOR
            Container
                .BindFactory<MockRoom, PlaceholderFactory<MockRoom>>()
                .FromComponentInNewPrefabResource(Consts.MockRoom);
#endif

            Container
                .BindInterfacesAndSelfTo<ARAggregate>()
                .AsSingle()
                .NonLazy();

            Container
                .Bind<ARPlaneRepository>()
                .AsSingle()
                .NonLazy();
        }
    }
}