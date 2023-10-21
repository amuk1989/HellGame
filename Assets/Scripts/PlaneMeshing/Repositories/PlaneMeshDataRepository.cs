using System;
using System.Collections.Generic;
using PlaneMeshing.Data;
using PlaneMeshing.Interfaces;
using UniRx;
using UnityEngine;

namespace PlaneMeshing.Repositories
{
    internal class PlaneMeshDataRepository: IPlaneMeshesProvider
    {
        private readonly ReactiveDictionary<Vector3Int, Mesh> _planeMeshes = new();

        public IObservable<PlaneMeshData> PlaneMeshUpdateAsObservable() => _planeMeshes
            .ObserveAdd()
            .Select(x => new PlaneMeshData(x.Key, x.Value))
            .Merge(_planeMeshes.ObserveReplace().Select(x => new PlaneMeshData(x.Key, x.NewValue)))
            .AsObservable();

        public IObservable<PlaneMeshData> PlaneMeshRemoveAsObservable() => _planeMeshes
            .ObserveRemove()
            .Select(x => new PlaneMeshData(x.Key, x.Value))
            .AsObservable();

        public IDictionary<Vector3Int, Mesh> PlaneMeshes => _planeMeshes;

        internal void AddPlane(Vector3Int id, Mesh mesh)
        {
            _planeMeshes[id] = mesh;
        }

        internal void RemovePlane(Vector3Int id)
        {
            if (!_planeMeshes.ContainsKey(id)) return;
            _planeMeshes.Remove(id);
        }
    }
}