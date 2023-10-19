using UnityEngine;

namespace AR.Data
{
    public struct UpdatedMeshData
    {
        public readonly Mesh Mesh;
        public readonly Vector3Int Id;

        public UpdatedMeshData(Mesh mesh, Vector3Int id)
        {
            Mesh = Object.Instantiate(mesh);
            Id = id;
        }
    }
}