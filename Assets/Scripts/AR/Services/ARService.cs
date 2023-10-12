using System;
using System.Collections.Generic;
using AR.Aggregates;
using AR.Data;
using AR.Interfaces;
using AR.Repositories;
using Niantic.ARDK.AR.Anchors;
using Niantic.ARDK.AR.ARSessionEventArgs;
using Niantic.ARDK.AR.Mesh;
using UniRx;
using UnityEngine;
using Zenject;

namespace AR.Services
{
    public class ARService: IARService, IARProvider, IDisposable, IInitializable
    {
        private readonly ARController _controller;
        private readonly ARPlaneRepository _arPlaneRepository;
        private readonly ARMeshRepository _arMeshRepository;

        internal ARService(ARController controller, ARPlaneRepository arPlaneRepository, ARMeshRepository arMeshRepository)
        {
            _controller = controller;
            _arPlaneRepository = arPlaneRepository;
            _arMeshRepository = arMeshRepository;
        }

        public bool IsInitialized => _controller.ARSession != null;

        public void Initialize()
        {
            
        }
        
        public void ARInitialize()
        {
            _controller.CreateARComponents();
        }

        public virtual void StartCollection()
        {
            if (!IsInitialized) return;
            
            _controller.ARSession!.AnchorsAdded += AddPlane;
            _controller.ARSession!.AnchorsMerged += MergePlanes;
            _controller.ARSession!.AnchorsRemoved += RemovePlane;
            _controller.ARSession!.AnchorsUpdated += UpdatePlane;

            _controller.ARMesh!.MeshBlocksUpdated += _arMeshRepository.UpdateMeshes;
            _controller.ARMesh!.MeshBlocksCleared += _arMeshRepository.ClearMeshes;
        }

        public void StopCollection()
        {
            if (_controller.ARSession == null) return;
            
            _controller.ARSession.AnchorsAdded -= AddPlane;
            _controller.ARSession.AnchorsMerged -= MergePlanes;
            _controller.ARSession.AnchorsRemoved -= RemovePlane;
            _controller.ARSession.AnchorsUpdated -= UpdatePlane;
            
            _controller.ARMesh!.MeshBlocksUpdated -= _arMeshRepository.UpdateMeshes;
            _controller.ARMesh!.MeshBlocksCleared -= _arMeshRepository.ClearMeshes;
        }

        public void Dispose()
        {
            StopCollection();
        }

        public IObservable<PlaneData> OnPlaneUpdated => _arPlaneRepository
            .OnPlaneUpdated
            .Merge(_arPlaneRepository.OnPlaneAdded);

        public IObservable<PlaneData> OnPlaneRemoved => _arPlaneRepository.OnPlaneRemoved;
        public IObservable<Unit> OnMeshUpdated => _arMeshRepository.OnMeshUpdated;
        public IReadOnlyList<PlaneData> Planes => _arPlaneRepository.Planes;
        public IEnumerable<Mesh> Meshes => _arMeshRepository.Meshes;
        public IEnumerable<Vector3> MeshPositions => _arMeshRepository.MeshGlobalPositions;

        private void AddPlane(AnchorsArgs args)
        {
            foreach (var arAnchor in args.Anchors)
            {
                if (arAnchor is not IARPlaneAnchor anchor) continue;
                _arPlaneRepository.AddPlane(anchor.Identifier.ToString(), anchor);
            }
        }

        private void MergePlanes(AnchorsMergedArgs args)
        {
            foreach (var arAnchor in args.Children)
            {
                var anchor = arAnchor as IARPlaneAnchor;
                if (anchor == null) continue;
                _arPlaneRepository.RemovePlane(anchor.Identifier.ToString());
            }

            var plane = args.Parent as IARPlaneAnchor;
            
            if (plane == null) return;
            
            _arPlaneRepository.AddPlane(plane.Identifier.ToString(), plane);
        }

        private void RemovePlane(AnchorsArgs args)
        {
            foreach (var arAnchor in args.Anchors)
            {
                _arPlaneRepository.RemovePlane(arAnchor.Identifier.ToString());
            }
        }

        private void UpdatePlane(AnchorsArgs args)
        {
            foreach (var arAnchor in args.Anchors)
            {
                if (arAnchor is not IARPlaneAnchor anchor) continue;
                _arPlaneRepository.UpdatePlane(anchor.Identifier.ToString(), anchor);
            }
        }
    }
}