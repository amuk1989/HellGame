using GameStage.Data;
using GameStage.Interfaces;
using Zenject;

namespace GameStage.Factories
{
    internal class GameStageFactory: PlaceholderFactory<GameStageId,IGameStage>
    {
    }

    internal class GameStageInstanceFactory : IFactory<GameStageId, IGameStage>
    {
        private readonly DiContainer _diContainer;

        public GameStageInstanceFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public IGameStage Create(GameStageId id)
        {
            return _diContainer.TryResolveId<IGameStage>(id);
        }
    }
}