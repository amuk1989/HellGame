using AR.Controllers;
using AR.Data;
using AR.Factories;
using AR.Interfaces;
using AR.Models;
using AR.Repositories;
using AR.Services;
using AR.View;
using UnityEngine;
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
                .BindInterfacesAndSelfTo<ARController>()
                .AsSingle()
                .NonLazy();

            Container
                .Bind<ARPlaneRepository>()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindInterfacesAndSelfTo<ARMeshRepository>()
                .AsSingle()
                .NonLazy();

            Container
                .BindFactory<PlaneData, PlaneModel, PlaneModel.Factory>()
                .FromFactory<PlaneModelFactory>();
            
            if (_arDebugConfig == null) return;

            Container
                .BindInterfacesTo<ARDebugger>()
                .AsSingle()
                .NonLazy();

            Container
                .BindFactory<PlaneModel, AnchorView, AnchorView.Factory>()
                .FromFactory<AnchorViewFactory>();
            
            Container
                .BindFactory<PlaneModel, AnchorDebugInfo, AnchorDebugInfo.Factory>()
                .FromFactory<AnchorDebugInfoFactory>();
        }
    }
}