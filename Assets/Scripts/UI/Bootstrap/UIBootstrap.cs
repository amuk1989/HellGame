using UI.Factories;
using UI.Interfaces;
using UI.Services;
using UI.Views;
using UnityEngine;
using Utility;
using Zenject;

namespace UI.Bootstrap
{
    public class UIBootstrap: Installer
    {
        public override void InstallBindings()
        {
            Container
                .BindFactory<Transform, StartGameUI, StartGameUI.Factory>()
                .FromFactory<StartGamePrefabFactory>();

            Container
                .Bind<UIComponent>()
                .FromComponentInNewPrefabResource(Consts.MainCanvas)
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesTo<UIService>()
                .AsSingle()
                .NonLazy();
        }
    }
}