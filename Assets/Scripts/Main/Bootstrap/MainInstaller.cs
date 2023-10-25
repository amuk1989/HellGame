using AR.Bootstrap;
using Debuger.Bootstrap;
using EnvironmentSystem.Bootstrap;
using GameStage.Bootstrap;
using PlaneMeshing.Bootstrap;
using Scanning.Bootstrap;
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
            Container.Install<ScanningInstaller>();
            Container.Install<EnvironmentInstaller>();
            Container.Install<PlaneMeshingInstaller>();
            Container.Install<GameStageInstaller>();
            Container.Install<DebugInstaller>();
        }
    }
}