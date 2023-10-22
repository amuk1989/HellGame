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
        private readonly PlaneView.Factory _planeFactory;
        private readonly RoomConfigData _roomConfig;

        public HolesRepository(PlaneView.Factory planeFactory, RoomConfigData roomConfig)
        {
            _planeFactory = planeFactory;
            _roomConfig = roomConfig;
        }

        public void AddHole(Mesh mesh)
        {
            _planeFactory.Create(_roomConfig.StencilLayerMask,_roomConfig.HolesMaterial, mesh);
        }
    }
}