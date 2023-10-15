using System;
using AR.Interfaces;
using PlaneMeshing.Aggregates;
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

        private PlaneRecognizer _planeRecognizer;
        private IARProvider _arProvider;

        [Inject]
        private void Construct(PlaneRecognizer planeRecognizer, IARProvider arProvider)
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

            _arProvider
                .OnPlaneUpdated
                .Subscribe(plane => _text.text += $"Plane id {plane.ID} {plane.Center}, {plane.Extends}{Environment.NewLine}")
                .AddTo(this);
        }
    }
}