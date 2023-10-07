using System;
using AR.Interfaces;
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

        private IARService _arService;

        [Inject]
        private void Construct(IARService arService)
        {
            _arService = arService;
        }

        private void Start()
        {
            _startGameButton
                .OnClickAsObservable()
                .Subscribe(_ => _arService.Initialize())
                .AddTo(this);
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}