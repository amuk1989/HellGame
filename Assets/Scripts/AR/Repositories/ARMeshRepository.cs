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
        private readonly Dictionary<Vector3Int, Vector3> _meshGlobalPositions = new();

        public IEnumerable<Mesh> Meshes => _meshes.Values;
        public IEnumerable<Vector3> MeshGlobalPositions => _meshGlobalPositions.Values;
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
                _meshes.Remove(block);
                _meshGlobalPositions.Remove(block);
            }
            
            foreach (var block in args.BlocksUpdated)
            {
                _meshes[block] = args.Mesh.Blocks[block].Mesh;
                _meshGlobalPositions[block] = new Vector3(block.x * 1.4f, block.y * -1.4f, block.z * 1.4f);
            }
        }
        
        internal void ClearMeshes(MeshBlocksClearedArgs args)
        {
            _meshes.Clear();
        }
    }
}