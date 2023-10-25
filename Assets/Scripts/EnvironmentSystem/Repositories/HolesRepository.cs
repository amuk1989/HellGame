using System.Collections.Generic;
using EnvironmentSystem.Data;
using EnvironmentSystem.Interfaces;
using EnvironmentSystem.View;
using PlaneMeshing.View;
using UnityEngine;

namespace EnvironmentSystem.Repositories
{
    internal class HolesRepository
    {
        private readonly List<PlaneView> _planeViews = new();
        private readonly HoleView.Factory _planeFactory;
        private readonly RoomConfigData _roomConfig;
        private readonly IPortalTextureProvider _portal;

        public HolesRepository(HoleView.Factory planeFactory, RoomConfigData roomConfig, IPortalTextureProvider portal)
        {
            _planeFactory = planeFactory;
            _roomConfig = roomConfig;
            _portal = portal;
        }

        public void AddHole(Mesh mesh)
        {
            _planeFactory.Create(_portal.Texture, _roomConfig.HolesMaterial, mesh);
        }
    }
}