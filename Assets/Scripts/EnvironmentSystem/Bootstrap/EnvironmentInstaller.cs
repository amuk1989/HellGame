using EnvironmentSystem.Repositories;
using EnvironmentSystem.Services;
using EnvironmentSystem.View;
using UnityEngine;
using Utility;
using Zenject;

namespace EnvironmentSystem.Bootstrap
{
    public class EnvironmentInstaller: Installer
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<RoomService>()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindFactory<Vector3, Quaternion, EnvironmentView, EnvironmentView.Factory>()
                .FromComponentInNewPrefabResource(Consts.Environment);
            
            Container
                .BindInterfacesTo<PortalService>()
                .AsSingle()
                .NonLazy();

            Container
                .Bind<HolesRepository>()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindFactory<RenderTexture, Material, Mesh, HoleView, HoleView.Factory>()
                .FromComponentInNewPrefabResource(Consts.Hole);
        }
    }
}