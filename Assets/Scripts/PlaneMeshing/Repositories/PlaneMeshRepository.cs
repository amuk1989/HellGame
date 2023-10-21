using System;
using System.Collections.Generic;
using PlaneMeshing.Data;
using PlaneMeshing.Interfaces;
using PlaneMeshing.View;
using UniRx;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Zenject;

namespace PlaneMeshing.Repositories
{
    internal class PlaneMeshRepository: IInitializable, IDisposable
    {
        private readonly Dictionary<Vector3Int, PlaneView> _planeMeshes = new();
        private readonly CompositeDisposable _compositeDisposable = new();

        private readonly PlaceholderFactory<Material, Mesh, PlaneView> _planeFactory;
        private readonly IPlaneMeshesProvider _planeMeshDataRepository;
        private readonly PlaneMeshingConfigData _config;

        internal PlaneMeshRepository(PlaceholderFactory<Material, Mesh, PlaneView> planeFactory,
            IPlaneMeshesProvider planeMeshDataRepository, PlaneMeshingConfigData config)
        {
            _planeFactory = planeFactory;
            _planeMeshDataRepository = planeMeshDataRepository;
            _config = config;
        }

        public void Initialize()
        {
            _planeMeshDataRepository
                .PlaneMeshRemoveAsObservable()
                .Subscribe(data => RemovePlane(data.Key))
                .AddTo(_compositeDisposable);

            _planeMeshDataRepository
                .PlaneMeshUpdateAsObservable()
                .Subscribe(data => AddPlane(data.Key, data.Value))
                .AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }

        private void AddPlane(Vector3Int id, Mesh plane)
        {
            if (_planeMeshes.TryGetValue(id, out var view)) view.Dispose();

            _planeMeshes[id] = _planeFactory.Create(_config.PlaneMaterial, plane);
        }

        private void RemovePlane(Vector3Int id)
        {
            if (!_planeMeshes.ContainsKey(id)) return;
            _planeMeshes.Remove(id);
        }
    }
}