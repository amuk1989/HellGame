using System;
using AR.Models;
using UniRx;
using UnityEngine;
using Zenject;

namespace AR.View
{
    public class AnchorView : MonoBehaviour, IDisposable
    {
        public class Factory: PlaceholderFactory<PlaneModel, AnchorView>
        {
        }
        
        [SerializeField] private Transform _plane;

        private PlaneModel _model;

        [Inject]
        private void Construct(PlaneModel model)
        {
            _model = model;
        }

        private void Start()
        {
            _model
                .PlaneDataAsObservable()
                .Subscribe(data =>
                {
                    SetSize(data.Extends);
                    transform.SetPositionAndRotation(data.Center, data.Rotation);
                })
                .AddTo(this);
            
            _model
                .OnDestroyedAsObservable()
                .Subscribe(_ => Dispose())
                .AddTo(this);
        }

        private void SetSize(Vector3 size)
        {
            _plane.localScale = size + Vector3.up;
        }
        
        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}