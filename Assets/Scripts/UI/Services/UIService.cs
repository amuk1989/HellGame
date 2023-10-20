using GameStage.Data;
using GameStage.Interfaces;
using UI.Interfaces;
using UI.Views;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI.Services
{
    public class UIService: IUiService
    {
        private readonly UIComponent _uiComponent;
        private readonly StartGameUI.Factory _startGameUiFactory;
        private readonly IGameStageService _gameStageService;

        private readonly CompositeDisposable _compositeDisposable = new();

        private UIService(UIComponent uiComponent, StartGameUI.Factory startGameUiFactory, IGameStageService gameStageService)
        {
            _uiComponent = uiComponent;
            _startGameUiFactory = startGameUiFactory;
            _gameStageService = gameStageService;
        }

        public void Initialize()
        {
            _gameStageService
                .GameStageAsObservable()
                .Where(x => x == GameStageId.StartMenu)
                .Subscribe(_ => _startGameUiFactory.Create(_uiComponent.Transform))
                .AddTo(_compositeDisposable);
        }

        public void SetOnCanvas(RectTransform view)
        {
            view.SetParent(_uiComponent.Transform);
        }
    }
}