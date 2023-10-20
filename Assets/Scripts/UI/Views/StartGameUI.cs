using System;
using AR.Interfaces;
using GameStage.Interfaces;
using Scanning.Interfaces;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Views
{
    public class StartGameUI: MonoBehaviour, IDisposable
    {
        public class Factory: PlaceholderFactory<Transform, StartGameUI>
        {
        }

        [SerializeField] private Button _startGameButton;

        private IGameStageService _gameStageService;

        [Inject]
        private void Construct(IGameStageService gameStageService)
        {
            _gameStageService = gameStageService;
        }

        private void Start()
        {
            _startGameButton
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    _gameStageService.NextStage();
                    Dispose();
                })
                .AddTo(this);
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}