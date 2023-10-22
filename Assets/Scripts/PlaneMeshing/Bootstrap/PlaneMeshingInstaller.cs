using PlaneMeshing.Controllers;
using PlaneMeshing.Factories;
using PlaneMeshing.Repositories;
using PlaneMeshing.View;
using UnityEngine;
using Zenject;

namespace PlaneMeshing.Bootstrap
{
    public class PlaneMeshingInstaller: Installer
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<PlaneRecognizer>()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindFactory<LayerMask, Material, Mesh, PlaneView, PlaneView.Factory>()
                .FromFactory<PlaneFactory>();

            Container
                .BindInterfacesAndSelfTo<PlaneMeshRepository>()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindInterfacesAndSelfTo<PlaneMeshDataRepository>()
                .AsSingle()
                .NonLazy();
        }
    }
}