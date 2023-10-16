using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace PlaneMeshing.Jobs
{
    public struct AntialiasingPlaneJob: IJobParallelFor
    {
        public NativeArray<Vector3> Vertices;
        public float PlaneYPosition;
        [ReadOnly] public Quaternion PlaneRotation;

        public void Execute(int index)
        {
            var position = Quaternion.Inverse(PlaneRotation) * Vertices[index];
            position.y = PlaneYPosition;
            Vertices[index] = PlaneRotation * position;
            // Vertices[index] = new Vector3(Vertices[index].x,PlaneYPosition,Vertices[index].z);
        }
    }
}