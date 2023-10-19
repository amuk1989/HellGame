using System;
using AR.Data;
using UniRx;
using Zenject;

namespace AR.Models
{
    public class PlaneModel: IDisposable
    {
        public class Factory: PlaceholderFactory<PlaneData, PlaneModel>
        {
        }
        
        private readonly ReactiveProperty<PlaneData> _planeData = new();
        private readonly ReactiveCommand _onDestroyed = new();

        public PlaneModel(PlaneData planeData)
        {
            _planeData.Value = planeData;
        }

        public PlaneData PlaneData => _planeData.Value;
        public IObservable<Unit> OnDestroyedAsObservable() => _onDestroyed.AsObservable();
        public IObservable<PlaneData> PlaneDataAsObservable() => _planeData.AsObservable();

        public void Update(PlaneData planeData)
        {
            _planeData.Value = planeData;
        }

        public void Dispose()
        {
            _onDestroyed.Execute();
        }
    }
}