using System.Collections.Generic;
using UnityEngine;

namespace PlaneMeshing.Interfaces
{
    public interface IPlaneMeshesProvider
    {
        public IDictionary<Vector3Int, Mesh> PlaneMeshes { get; }
    }
}