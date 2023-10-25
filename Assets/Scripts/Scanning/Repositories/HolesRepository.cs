using System.Collections.Generic;
using PlaneMeshing.Factories;
using PlaneMeshing.View;
using Scanning.Data;
using Scanning.Interfaces;
using Scanning.View;
using UnityEngine;
using Zenject;

namespace Scanning.Repositories
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