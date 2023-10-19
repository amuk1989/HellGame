using System;
using System.Collections.Generic;
using AR.Data;
using AR.Models;
using UniRx;
using UnityEngine;

namespace AR.Interfaces
{
    public interface IARProvider
    {
        public IObservable<PlaneModel> OnPlaneUpdated { get; }
        public IObservable<PlaneModel> OnPlaneRemoved { get; }
        public IReadOnlyList<PlaneModel> Planes { get; }
        public IEnumerable<UpdatedMeshData> Meshes { get; }
        public IObservable<UpdatedMeshData> OnMeshUpdated { get; }
        public IObservable<UpdatedMeshData> OnMeshRemoved { get; }
    }
}