using System.Collections.Generic;
using PlaneMeshing.View;
using UnityEngine;
using Zenject;

namespace PlaneMeshing.Repositories
{
    internal class PlaneMeshRepository
    {
        private readonly Dictionary<Vector3Int, PlaneView> _planeMeshes = new();

        private readonly PlaceholderFactory<Mesh, PlaneView> _planeFactory;

        internal PlaneMeshRepository(PlaceholderFactory<Mesh, PlaneView> planeFactory)
        {
            _planeFactory = planeFactory;
        }

        internal void AddPlane(Vector3Int id, Mesh plane)
        {
            if (_planeMeshes.TryGetValue(id, out var view)) view.Dispose();

            _planeMeshes[id] = _planeFactory.Create(plane);
        }

        internal void RemovePlane(Vector3Int id)
        {
            if (!_planeMeshes.ContainsKey(id)) return;
            _planeMeshes.Remove(id);
        }
    }
}