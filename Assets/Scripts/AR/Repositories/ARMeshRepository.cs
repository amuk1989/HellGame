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
        private readonly ReactiveDictionary<Vector3Int, Mesh> _meshes = new();

        public IEnumerable<Mesh> Meshes => _meshes.Values;
        public IObservable<UpdatedMeshData> OnMeshUpdated { get; private set; }
        public IObservable<UpdatedMeshData> OnMeshRemoved { get; private set; }

        public void Initialize()
        {
            OnMeshUpdated = _meshes
                .ObserveAdd()
                .Select(x => new UpdatedMeshData(x.Value, x.Key))
                .Merge(_meshes
                        .ObserveReplace()
                        .Select(x => new UpdatedMeshData(x.NewValue, x.Key)))
                .AsObservable();

            OnMeshRemoved = _meshes
                .ObserveRemove()
                .Select(x => new UpdatedMeshData(x.Value, x.Key))
                .AsObservable();
        }

        internal void UpdateMeshes(MeshBlocksUpdatedArgs args)
        {
            foreach (var block in args.BlocksObsoleted)
            {
                if (!_meshes.ContainsKey(block)) continue;
                _meshes[block].Clear();
                _meshes.Remove(block);
            }
            
            foreach (var block in args.BlocksUpdated)
            {
                _meshes[block] = args.Mesh.Blocks[block].Mesh;
            }
        }
        
        internal void ClearMeshes(MeshBlocksClearedArgs args)
        {
            foreach (var mesh in _meshes)
            {
                mesh.Value.Clear();
            }

            _meshes.Clear();
        }
    }
}