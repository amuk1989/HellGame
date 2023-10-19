using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace PlaneMeshing.Jobs
{
    public struct AntialiasingPlaneJob: IJobParallelFor
    {
        public NativeArray<Vector3> Vertices;
        [ReadOnly] public float3 PlanePosition;
        [ReadOnly] public Quaternion PlaneRotation;
        [ReadOnly] public float Offset;

        public void Execute(int index)
        {
            var position = Quaternion.Inverse(PlaneRotation) * Vertices[index];
            
            position.y = (Quaternion.Inverse(PlaneRotation) * PlanePosition).y+Offset;
            Vertices[index] = PlaneRotation * position;
        }
    }
}