using UI.Views;
using UnityEngine;
using Utility;
using Zenject;

namespace UI.Factories
{
    public class StartGamePrefabFactory: IFactory<Transform, StartGameUI>
    {
        private readonly DiContainer _diContainer;

        public StartGamePrefabFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public StartGameUI Create(Transform param)
        {
            return _diContainer
                .InstantiatePrefabResourceForComponent<StartGameUI>(Consts.StartUi, param);
        }
    }
}