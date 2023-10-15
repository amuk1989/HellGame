using PlaneMeshing.Utilities;
using Unity.Mathematics;
using UnityEngine;

namespace AR.Data
{
    public struct PlaneData
    {
        public readonly float3 Center;
        public readonly float3 Extends;
        public readonly quaternion Rotation;
        public readonly PlaneOrientation PlaneOrientation;
        public readonly string ID;

        public PlaneData(float3 center, float3 extends, string id, quaternion rotation, PlaneOrientation planeOrientation)
        {
            Center = center;
            Extends = extends;
            ID = id;
            Rotation = rotation;
            PlaneOrientation = planeOrientation;
        }
    }
}