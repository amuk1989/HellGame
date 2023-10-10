using System;
using System.Collections.Generic;
using AR.Data;
using UniRx;
using UnityEngine;

namespace AR.Interfaces
{
    public interface IARProvider
    {
        public IObservable<PlaneData> OnPlaneUpdated { get; }
        public IObservable<PlaneData> OnPlaneRemoved { get; }
        public IObservable<Unit> OnMeshUpdated { get; }
        public IReadOnlyList<PlaneData> Planes { get; }
        public IEnumerable<Mesh> Meshes { get; }
        public IEnumerable<Vector3> MeshPositions { get; }
    }
}