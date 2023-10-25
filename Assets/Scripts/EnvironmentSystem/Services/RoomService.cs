using System.Collections.Generic;
using AR.Interfaces;
using EnvironmentSystem.Interfaces;
using EnvironmentSystem.Repositories;
using EnvironmentSystem.View;
using PlaneMeshing.Interfaces;
using UnityEngine;
using Zenject;

namespace EnvironmentSystem.Services
{
    internal class RoomService: IRoomService, IInitializable
    {
        private readonly IPlaneMeshesProvider _planeMeshes;
        private readonly HolesRepository _holesRepository;
        private readonly ICameraProvider _camera;
        private readonly EnvironmentView.Factory _factory;
        
        public RoomService(IPlaneMeshesProvider planeMeshes, HolesRepository holesRepository, ICameraProvider camera,
            EnvironmentView.Factory factory)
        {
            _planeMeshes = planeMeshes;
            _holesRepository = holesRepository;
            _camera = camera;
            _factory = factory;
        }

        public void Initialize()
        {
        }

        public void CreateHoles()
        {
            foreach (var plane in _planeMeshes.PlaneMeshes)
            {
                _holesRepository.AddHole(plane.Value);
            }
        }

        public void CreateEnvironment()
        {
            var vertices = new List<Vector3>();

            foreach (var mesh in _planeMeshes.PlaneMeshes.Values)
            {
                foreach (var vertex in mesh.vertices)
                {
                    if (_camera.IsPointInView(vertex)) vertices.Add(vertex);
                }
            }

            var average = Vector3.zero;

            foreach (var vertex in vertices)
            {
                average += vertex;
            }

            average = new Vector3(average.x / vertices.Count, _camera.CameraPosition.y - 1f, average.z / vertices.Count);

            var point = _camera.CameraPosition + _camera.CameraSightDirection;
            var environment = _factory.Create();
            environment.transform.SetPositionAndRotation(average, _camera.CameraRotation);
        }
    }
}