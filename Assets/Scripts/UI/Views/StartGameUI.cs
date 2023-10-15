using System;
using AR.Interfaces;
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
        [SerializeField] private Button _stopScanButton;

        private IScanningService _scanningService;

        [Inject]
        private void Construct(IScanningService scanningService)
        {
            _scanningService = scanningService;
        }

        private void Start()
        {
            _startGameButton
                .OnClickAsObservable()
                .Subscribe(_ => _scanningService.AsyncScanningTask())
                .AddTo(this);
            
            _stopScanButton
                .OnClickAsObservable()
                .Subscribe(_ => _scanningService.AsyncScanningTask())
                .AddTo(this);
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}