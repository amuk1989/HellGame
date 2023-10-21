using UI.Factories;
using UI.Interfaces;
using UI.Rules;
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
                .BindFactory<string, Transform, BaseUI, BaseUI.Factory>()
                .FromFactory<UIPrefabFactory>();

            Container
                .Bind<UIComponent>()
                .FromComponentInNewPrefabResource(Consts.MainCanvas)
                .AsSingle()
                .NonLazy();

            Container.InstallServiceAsInterface<UIService>();
            Container.InstallServiceAsInterface<UIRule>();
        }
    }
}