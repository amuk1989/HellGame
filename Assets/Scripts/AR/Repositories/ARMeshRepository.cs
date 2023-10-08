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
        private readonly ReactiveCommand _onMeshUpdated = new();

        public IEnumerable<Mesh> Meshes => _meshes.Values;

        public IObservable<Unit> OnMeshUpdated => _onMeshUpdated.AsObservable();

        internal void UpdateMeshes(MeshBlocksUpdatedArgs args)
        {
            foreach (var block in args.BlocksObsoleted)
            {
                if (!_meshes.ContainsKey(block)) continue;
                _meshes.Remove(block);
            }
            
            foreach (var block in args.BlocksUpdated)
            {
                _meshes[block] = args.Mesh.Blocks[block].Mesh;
            }

            _onMeshUpdated.Execute();
        }
        
        internal void ClearMeshes(MeshBlocksClearedArgs args)
        {
            _meshes.Clear();
        }
    }
}