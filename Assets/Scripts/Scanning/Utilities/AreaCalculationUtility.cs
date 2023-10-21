using PlaneMeshing.Utilities;
using UnityEngine;

namespace Scanning.Utilities
{
    public static class AreaCalculationUtility
    {
        public static float CalculatePlaneArea(Mesh mesh)
        {
            var result = 0f;
            
            for (int i = 0; i < mesh.triangles.Length; i+=3)
            {
                var a = mesh.vertices[mesh.triangles[i]];
                var b = mesh.vertices[mesh.triangles[i+1]];
                var c = mesh.vertices[mesh.triangles[i+2]];
                result += MeshingUtility.TriangleArea(a, b, c);
            }

            return result;
        }
    }
}