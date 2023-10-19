using System.ComponentModel;
using UnityEngine;
using Zenject;

namespace Utility
{
    public static class ZenjectExtensions
    {
        public static void InstallServiceAsInterface<TService>(this DiContainer container)
        {
            container
                .BindInterfacesTo<TService>()
                .AsSingle()
                .NonLazy();
        }
        
        public static void InstallRegistry<TRegistry>(this DiContainer container, TRegistry registry) where TRegistry: ScriptableObject
        {
            container
                .Bind<TRegistry>()
                .FromInstance(registry);
        }
    }
}