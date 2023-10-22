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
    public class NextStageUI: BaseUI, IDisposable
    {
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
    }
}