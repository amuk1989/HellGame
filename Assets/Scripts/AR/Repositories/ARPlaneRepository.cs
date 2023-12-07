using System;
using System.Collections.Generic;
using System.Linq;
using AR.Data;
using AR.Models;
using PlaneMeshing.Utilities;
using UniRx;

namespace AR.Repositories
{
    public class ARPlaneRepository
    {
        private readonly ReactiveDictionary<string, PlaneModel> _planes = new();

        private readonly PlaneModel.Factory _modelFactory;

        public ARPlaneRepository(PlaneModel.Factory modelFactory)
        {
            _modelFactory = modelFactory;
        }

        public IReadOnlyList<PlaneModel> Planes => _planes.Values.ToList();
        public IObservable<PlaneModel> OnPlaneAdded => _planes.ObserveAdd().AsObservable().Select(x=> x.Value);
        public IObservable<PlaneModel> OnPlaneUpdated => _planes.ObserveReplace().AsObservable().Select(x=> x.NewValue);
        public IObservable<PlaneModel> OnPlaneRemoved => _planes.ObserveRemove().AsObservable().Select(x=> x.Value);

        public void AddPlane(string id, IARPlaneAnchor planeAnchor)
        {
            var data = new PlaneData(planeAnchor.Transform.ToPosition(), planeAnchor.Extent, id, 
                planeAnchor.Transform.ToRotation(), (PlaneOrientation)planeAnchor.Alignment);
            _planes[id] = _modelFactory.Create(data);
        }
        
        public void UpdatePlane(string id, IARPlaneAnchor planeAnchor)
        {
            var data = new PlaneData(planeAnchor.Transform.ToPosition(), planeAnchor.Extent, id,
                planeAnchor.Transform.ToRotation(), (PlaneOrientation)planeAnchor.Alignment);
            _planes[id].Update(data);
        }

        public void RemovePlane(string id)
        {
            if (!_planes.TryGetValue(id, out var plane)) return;
            plane.Dispose();
            _planes.Remove(id);
        }
    }
}