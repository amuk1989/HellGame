using System.Collections.Generic;
using PlaneMeshing.Factories;
using PlaneMeshing.View;
using Scanning.Data;
using UnityEngine;

namespace Scanning.Repositories
{
    internal class HolesRepository
    {
        private readonly List<PlaneView> _planeViews = new();
        private readonly PlaneFactory _planeFactory;
        private readonly RoomConfigData _roomConfig;

        public HolesRepository(PlaneFactory planeFactory, RoomConfigData roomConfig)
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