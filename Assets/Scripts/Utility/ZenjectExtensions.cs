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
    }
}