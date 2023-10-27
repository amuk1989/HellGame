using UnityEngine;

namespace AR.Data
{
    public struct UpdatedMeshData
    {
        public readonly int[] Triangles;
        public readonly Vector3[] Vertices;
        public readonly Vector3Int Id;

        public UpdatedMeshData(Mesh mesh, Vector3Int id)
        {
            Triangles = mesh.triangles;
            Vertices = mesh.vertices;
            Id = id;
        }
    }
}