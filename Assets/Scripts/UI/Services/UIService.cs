using UI.Interfaces;
using UI.Views;
using UnityEngine;
using Zenject;

namespace UI.Services
{
    public class UIService: IUiService, IInitializable
    {
        private readonly UIComponent _uiComponent;
        private readonly StartGameUI.Factory _startGameUiFactory;

        private UIService(UIComponent uiComponent, StartGameUI.Factory startGameUiFactory)
        {
            _uiComponent = uiComponent;
            _startGameUiFactory = startGameUiFactory;
        }

        public void Initialize()
        {
            CreateStartMenu();
        }

        public void CreateStartMenu()
        {
            _startGameUiFactory.Create(_uiComponent.Transform);
        }

        public void SetOnCanvas(RectTransform view)
        {
            view.SetParent(_uiComponent.Transform);
        }
    }
}