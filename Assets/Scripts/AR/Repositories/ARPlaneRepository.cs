using System;
using System.Collections.Generic;
using System.Linq;
using AR.Data;
using Niantic.ARDK.AR.Anchors;
using Niantic.ARDK.Utilities;
using UniRx;

namespace AR.Repositories
{
    public class ARPlaneRepository
    {
        private readonly ReactiveDictionary<string, PlaneData> _planes = new();
        
        public IReadOnlyList<PlaneData> Planes => _planes.Values.ToList();

        public IObservable<PlaneData> OnPlaneAdded => _planes.ObserveAdd().AsObservable().Select(x=> x.Value);
        public IObservable<PlaneData> OnPlaneRemoved => _planes.ObserveRemove().AsObservable().Select(x=> x.Value);
        public IObservable<PlaneData> OnPlaneUpdated => _planes.ObserveReplace().AsObservable().Select(x=> x.NewValue);

        public void AddPlane(string id, IARPlaneAnchor planeAnchor)
        {
            _planes[id] = new PlaneData(planeAnchor.Transform.ToPosition(), planeAnchor.Extent, id, planeAnchor.Transform.ToRotation());
        }
        
        public void UpdatePlane(string id, IARPlaneAnchor planeAnchor)
        {
            _planes[id] = new PlaneData(planeAnchor.Transform.ToPosition(), planeAnchor.Extent, id,planeAnchor.Transform.ToRotation());
        }

        public void RemovePlane(string id)
        {
            if (!_planes.ContainsKey(id)) return;
            _planes.Remove(id);
        }
    }
}