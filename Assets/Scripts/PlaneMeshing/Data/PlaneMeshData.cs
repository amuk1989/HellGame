using UnityEngine;

namespace PlaneMeshing.Data
{
    public struct PlaneMeshData
    {
        public readonly Vector3Int Key;
        public readonly Mesh Value;

        public PlaneMeshData(Vector3Int key, Mesh value)
        {
            Key = key;
            Value = value;
        }
    }
}