using PlaneMeshing.Utilities;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace PlaneMeshing.Jobs
{
    public struct SearchTrianglesJob: IJobParallelForBatch
    {
        public NativeArray<int> ValidateTriangles;
        [ReadOnly] public NativeArray<Vector3> Vertices;
        [ReadOnly] public NativeArray<int> Triangles;
        [ReadOnly] public float3 AreaCenter;
        [ReadOnly] public float3 AreaBounce;
        [ReadOnly] public PlaneOrientation PlaneOrientations;
        [ReadOnly] public quaternion PlaneRotation;

        public void Execute(int index, int count)
        {
            for (int i = index; i < index+count; i++)
            {
                ValidateTriangles[i] = -1;
            }

            for (int i = index; i < index + count; i++)
            {
                if (!MeshingUtility.IsInsidePoint(Vertices[Triangles[i]], AreaCenter, AreaBounce, PlaneRotation)) return;
            }

            for (int i = index; i < index+count; i++)
            {
                ValidateTriangles[i] = Triangles[i];
            }
        }
    }
}