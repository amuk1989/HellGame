using Unity.Mathematics;

namespace AR.Data
{
    public struct PlaneData
    {
        public readonly float3 Center;
        public readonly float3 Extends;
        public readonly string ID;

        public PlaneData(float3 center, float3 extends, string id)
        {
            Center = center;
            Extends = extends;
            ID = id;
        }
    }
}