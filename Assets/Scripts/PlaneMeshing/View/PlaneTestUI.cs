using System;
using PlaneMeshing.Aggregates;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PlaneMeshing.View
{
    public class PlaneTestUI: MonoBehaviour
    {
        [SerializeField] private Button _testButton;

        private PlaneRecognizer _planeRecognizer;

        [Inject]
        private void Construct(PlaneRecognizer planeRecognizer)
        {
            _planeRecognizer = planeRecognizer;
        }

        private void Start()
        {
            _testButton
                .OnClickAsObservable()
                .Subscribe(_ => _planeRecognizer.Recognize())
                .AddTo(this);
        }
    }
}