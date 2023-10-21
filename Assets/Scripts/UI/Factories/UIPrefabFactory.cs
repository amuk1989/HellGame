using UI.Views;
using UnityEngine;
using Utility;
using Zenject;

namespace UI.Factories
{
    public class UIPrefabFactory: IFactory<string, Transform, BaseUI>
    {
        private readonly DiContainer _diContainer;

        public UIPrefabFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public BaseUI Create(string path, Transform param)
        {
            return _diContainer
                .InstantiatePrefabResourceForComponent<BaseUI>(path, param);
        }
    }
}