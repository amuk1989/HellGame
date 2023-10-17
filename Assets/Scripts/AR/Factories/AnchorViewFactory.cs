using AR.Data;
using AR.Models;
using AR.View;
using Zenject;

namespace AR.Factories
{
    public class AnchorViewFactory: IFactory<PlaneModel, AnchorView>
    {
        private DiContainer _diContainer;
        private ARDebugConfig _config;

        public AnchorViewFactory(DiContainer diContainer, ARDebugConfig config)
        {
            _diContainer = diContainer;
            _config = config;
        }

        public AnchorView Create(PlaneModel param)
        {
            return _diContainer.InstantiatePrefabForComponent<AnchorView>(_config.AnchorView, new object[] {param});
        }
    }
}