using Unity.Mathematics;

namespace PlaneMeshing.Data
{
    public struct PlaneData
    {
        public readonly float3[] Vertices;
        public readonly int[] Triangles;

        public PlaneData(int[] triangles, float3[] vertices)
        {
            Triangles = triangles;
            Vertices = vertices;
        }
    }
}