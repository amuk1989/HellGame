using System;
using System.Collections.Generic;
using PlaneMeshing.Data;
using UnityEngine;

namespace PlaneMeshing.Interfaces
{
    public interface IPlaneMeshesProvider
    {
        public IDictionary<Vector3Int, Mesh> PlaneMeshes { get; }
        public IObservable<PlaneMeshData> PlaneMeshUpdateAsObservable();
        public IObservable<PlaneMeshData> PlaneMeshRemoveAsObservable();
    }
}