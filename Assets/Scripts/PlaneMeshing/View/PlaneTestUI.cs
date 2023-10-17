using System;
using AR.Interfaces;
using PlaneMeshing.Aggregates;
using PlaneMeshing.Interfaces;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PlaneMeshing.View
{
    public class PlaneTestUI: MonoBehaviour
    {
        [SerializeField] private Button _testButton;
        [SerializeField] private TMP_Text _text; 

        private IPlaneRecognizer _planeRecognizer;
        private IARProvider _arProvider;

        [Inject]
        private void Construct(IPlaneRecognizer planeRecognizer, IARProvider arProvider)
        {
            _planeRecognizer = planeRecognizer;
            _arProvider = arProvider;
        }

        private void Start()
        {
            _text.text = String.Empty;
            
            _testButton
                .OnClickAsObservable()
                .Subscribe(_ => _planeRecognizer.StartRecognizer())
                .AddTo(this);
        }
    }
}