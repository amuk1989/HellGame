using System.Collections.Generic;
using PlaneMeshing.Data;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

namespace PlaneMeshing.Utilities
{
    public static class MeshingUtility
    {
        public static NativeArray<int> GetInsideVertices(Mesh mesh, float3 bounceCenter, float3 bounceSize)
        {
            var verticesData = CheckInsideVertices(mesh, bounceCenter, bounceSize);

            return GetInsideVertices(new NativeArray<int>(mesh.triangles, Allocator.TempJob), verticesData.Item2,
                verticesData.Item1);
        }
        
        private static (int, NativeArray<bool>) CheckInsideVertices(Mesh mesh, float3 bounceCenter, float3 bounceSize)
        {
            NativeArray<bool> triangles = new(mesh.triangles.Length, Allocator.Temp);
            
            int count = 0;

            for (int i = 0; i < mesh.triangles.Length; i+=3)
            {
                triangles[i] = false;
                triangles[i+1] = false;
                triangles[i+2] = false;
                
                if (!IsInsidePoint(mesh.vertices[mesh.triangles[i]], bounceCenter, bounceSize)) continue;
                if (!IsInsidePoint(mesh.vertices[mesh.triangles[i+1]], bounceCenter, bounceSize)) continue;
                if (!IsInsidePoint(mesh.vertices[mesh.triangles[i+2]], bounceCenter, bounceSize)) continue;

                count+=3;

                triangles[i] = true;
                triangles[i+1] = true;
                triangles[i+2] = true;
            }

            return (count, triangles);
        }

        private static NativeArray<int> GetInsideVertices(NativeArray<int> originTriangles, 
            NativeArray<bool> isValidTriangles, int size)
        {
            var triangles = new NativeArray<int>(size, Allocator.TempJob);

            var index = 0;

            for (int i = 0; i < originTriangles.Length; i++)
            {
                if (isValidTriangles[i]) triangles[index++] = originTriangles[i];
            }

            return triangles;
        }

        public static bool IsInsidePoint(float3 point, float3 bounceCenter, float3 bounceSize)
        {
#if UNITY_EDITOR
            point.y *= -1;
#endif
            if (math.abs(point.y - bounceCenter.y) > 0.1f) return false;

            point.y = bounceCenter.y;

            var rect = GetRectData(bounceCenter, bounceSize);
            
            var pab = TriangleArea(point,rect.A,rect.B);
            var pbc = TriangleArea(point, rect.B, rect.C);
            var pcd = TriangleArea(point, rect.C, rect.D);
            var pad = TriangleArea(point, rect.A, rect.D);

            return math.abs(pab + pcd + pbc + pad - rect.Area) < 0.05f;
        }

        private static float TriangleArea(float3 a, float3 b, float3 c)
        {
            var ab = math.distance(a, b);
            var ac = math.distance(a, c);
            var bc = math.distance(b, c);

            var p = (ab + ac + bc) * 0.5f;

            return math.sqrt(p * (p - ab) * (p - ac) * (p - bc));
        }

        private static BounceRect GetRectData(float3 bounceCenter, float3 bounceSize)
        {
            var a = new float3(bounceCenter.x - bounceSize.x*0.5f, bounceCenter.y, bounceCenter.z + bounceSize.z*0.5f);
            var b = new float3(bounceCenter.x + bounceSize.x*0.5f, bounceCenter.y, bounceCenter.z + bounceSize.z*0.5f);
            var c = new float3(bounceCenter.x + bounceSize.x*0.5f, bounceCenter.y, bounceCenter.z - bounceSize.z*0.5f);
            var d = new float3(bounceCenter.x - bounceSize.x*0.5f, bounceCenter.y, bounceCenter.z - bounceSize.z*0.5f);
            
            var bounceArea = bounceSize.x * bounceSize.z;

            return new BounceRect()
            {
                A = a,
                B = b,
                C = c,
                D = d,
                Area = bounceArea
            };
        }
    }

    public struct BounceRect
    {
        public float3 A;
        public float3 B;
        public float3 C;
        public float3 D;
        public float Area;
    }
}