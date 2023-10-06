﻿using AR.Bootstrap;
using UI.Bootstrap;
using Zenject;

namespace Main.Bootstrap
{
    public class MainInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Install<UIBootstrap>();
            Container.Install<ARInstaller>();
        }
    }
}