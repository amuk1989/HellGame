using System;
using System.Collections.Generic;
using System.Linq;
using Niantic.ARDK.AR.Mesh;
using UniRx;
using UnityEngine;

namespace AR.Repositories
{
    internal class ARMeshRepository
    {
        private readonly ReactiveDictionary<Vector3Int, Mesh> _meshes = new();
        private readonly Dictionary<Vector3Int, Vector3> _meshGlobalPositions = new();
        private readonly ReactiveCommand _onMeshUpdated = new();

        public IEnumerable<Mesh> Meshes => _meshes.Values;
        public IEnumerable<Vector3> MeshGlobalPositions => _meshGlobalPositions.Values;

        public IObservable<Unit> OnMeshUpdated => _onMeshUpdated.AsObservable();

        internal void UpdateMeshes(MeshBlocksUpdatedArgs args)
        {
            foreach (var block in args.BlocksObsoleted)
            {
                if (!_meshes.ContainsKey(block)) continue;
                _meshes.Remove(block);
                _meshGlobalPositions.Remove(block);
            }
            
            foreach (var block in args.BlocksUpdated)
            {
                _meshes[block] = args.Mesh.Blocks[block].Mesh;
                _meshGlobalPositions[block] = new Vector3(block.x * 1.4f, block.y * -1.4f, block.z * 1.4f);
            }

            _onMeshUpdated.Execute();
        }
        
        internal void ClearMeshes(MeshBlocksClearedArgs args)
        {
            _meshes.Clear();
        }
    }
}