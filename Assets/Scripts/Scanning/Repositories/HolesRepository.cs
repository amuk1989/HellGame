using System.Collections.Generic;
using PlaneMeshing.Factories;
using PlaneMeshing.View;
using Scanning.Data;
using UnityEngine;
using Zenject;

namespace Scanning.Repositories
{
    internal class HolesRepository
    {
        private readonly List<PlaneView> _planeViews = new();
        private readonly PlaceholderFactory<Material, Mesh, PlaneView> _planeFactory;
        private readonly RoomConfigData _roomConfig;

        public HolesRepository(PlaceholderFactory<Material, Mesh, PlaneView> planeFactory, RoomConfigData roomConfig)
        {
            _planeFactory = planeFactory;
            _roomConfig = roomConfig;
        }

        public void AddHole(Mesh mesh)
        {
            _planeFactory.Create(_roomConfig.HolesMaterial, mesh);
        }
    }
}