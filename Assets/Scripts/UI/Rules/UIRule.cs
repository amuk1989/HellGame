using System;
using GameStage.Data;
using GameStage.Interfaces;
using Scanning.Interfaces;
using UI.Factories;
using UI.Views;
using UniRx;
using Utility;
using Zenject;

namespace UI.Rules
{
    internal class UIRule: IInitializable, IDisposable
    {
        private readonly CompositeDisposable _compositeDisposable = new();
        
        private readonly IGameStageService _gameStageService;
        private readonly UIComponent _uiComponent;
        private readonly BaseUI.Factory _uiPrefabFactory;
        private readonly IScanningService _scanningService;
        private BaseUI _progressUI;

        public UIRule(IGameStageService gameStageService, 
            UIComponent uiComponent, IScanningService scanningService, BaseUI.Factory uiPrefabFactory)
        {
            _gameStageService = gameStageService;
            _uiComponent = uiComponent;
            _scanningService = scanningService;
            _uiPrefabFactory = uiPrefabFactory;
        }

        public void Initialize()
        {
            _gameStageService
                .GameStageAsObservable()
                .Where(x => x == GameStageId.StartMenu)
                .Subscribe(_ => _uiPrefabFactory.Create(Consts.StartUi, _uiComponent.Transform))
                .AddTo(_compositeDisposable);
            
            _gameStageService
                .GameStageAsObservable()
                .Where(x => x == GameStageId.Scanning)
                .Subscribe(_ => _progressUI = _uiPrefabFactory.Create(Consts.ScanProgressUI, _uiComponent.Transform))
                .AddTo(_compositeDisposable);
            
            _gameStageService
                .GameStageAsObservable()
                .Where(x => x == GameStageId.Game)
                .Subscribe(_ => _progressUI.Dispose())
                .AddTo(_compositeDisposable);

            _scanningService
                .ScannedEnoughArea()
                .Subscribe(_ => _uiPrefabFactory.Create(Consts.ScanStopUI, _uiComponent.Transform))
                .AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
    }
}