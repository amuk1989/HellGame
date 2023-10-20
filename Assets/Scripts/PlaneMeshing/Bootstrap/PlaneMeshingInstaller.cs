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
                .BindFactory<Mesh, PlaneView, PlaceholderFactory<Mesh, PlaneView>>()
                .FromFactory<PlaneFactory>();

            Container
                .BindInterfacesAndSelfTo<PlaneMeshRepository>()
                .AsSingle()
                .NonLazy();
        }
    }
}