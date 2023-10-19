using System;
using System.Collections.Generic;
using System.Linq;
using AR.Data;
using Niantic.ARDK.AR.Mesh;
using UniRx;
using UnityEngine;
using Zenject;

namespace AR.Repositories
{
    internal class ARMeshRepository: IInitializable
    {
        private readonly ReactiveDictionary<Vector3Int, UpdatedMeshData> _meshes = new();

        public IEnumerable<UpdatedMeshData> Meshes => _meshes.Values;
        public IObservable<UpdatedMeshData> OnMeshUpdated { get; private set; }
        public IObservable<UpdatedMeshData> OnMeshRemoved { get; private set; }

        public void Initialize()
        {
            OnMeshUpdated = _meshes
                .ObserveAdd()
                .Select(x => x.Value)
                .Merge(_meshes
                        .ObserveReplace()
                        .Select(x => x.NewValue))
                .AsObservable();

            OnMeshRemoved = _meshes
                .ObserveRemove()
                .Select(x => x.Value)
                .AsObservable();
        }

        internal void UpdateMeshes(MeshBlocksUpdatedArgs args)
        {
            foreach (var block in args.BlocksObsoleted)
            {
                if (!_meshes.ContainsKey(block)) continue;
                _meshes[block].Mesh.Clear();
                _meshes.Remove(block);
            }
            
            foreach (var block in args.BlocksUpdated)
            {
                _meshes[block] = new UpdatedMeshData(args.Mesh.Blocks[block].Mesh, block);
            }
        }
        
        internal void ClearMeshes(MeshBlocksClearedArgs args)
        {
            foreach (var mesh in _meshes)
            {
                mesh.Value.Mesh.Clear();
            }

            _meshes.Clear();
        }
    }
}