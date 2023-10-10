using PlaneMeshing.Aggregates;
using PlaneMeshing.Factories;
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
                .BindInterfacesAndSelfTo<PlaneRecognizer>()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindFactory<Mesh, PlaneView, PlaceholderFactory<Mesh, PlaneView>>()
                .FromFactory<PlaneFactory>();
        }
    }
}