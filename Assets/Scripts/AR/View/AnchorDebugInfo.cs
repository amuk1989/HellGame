using System;
using AR.Data;
using AR.Interfaces;
using AR.Models;
using TMPro;
using UniRx;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace AR.View
{
    public class AnchorDebugInfo : MonoBehaviour
    {
        internal class Factory: PlaceholderFactory<PlaneModel, AnchorDebugInfo>
        {
        }
        
        [SerializeField] private TMP_Text _text;
        [SerializeField] private RectTransform _rectTransform;

        private PlaneModel _model;
        private ICameraProvider _cameraProvider;
        private PlaneData _planeData;

        [Inject]
        private void Construct(PlaneModel model, ICameraProvider cameraProvider)
        {
            _model = model;
            _cameraProvider = cameraProvider;
        }

        private void Start()
        {
            _model.PlaneDataAsObservable()
                .Subscribe(data =>
                {
                    _planeData = data;
                    _text.text = $"Center {_planeData.Center}{Environment.NewLine}" +
                                 $"Rotation {((Quaternion)_planeData.Rotation).eulerAngles}{Environment.NewLine}" +
                                 $"Orientation {_planeData.PlaneOrientation}";
                })
                .AddTo(this);

            _model.OnDestroyedAsObservable()
                .Subscribe(_ => Destroy(gameObject))
                .AddTo(this);
        }

        private void Update()
        {
            _rectTransform.position = _cameraProvider.ARCamera.WorldToScreenPoint(_planeData.Center);
        }
    }
}