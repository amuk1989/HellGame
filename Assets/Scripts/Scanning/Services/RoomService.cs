using PlaneMeshing.Factories;
using PlaneMeshing.Interfaces;
using Scanning.Data;
using Scanning.Interfaces;
using Zenject;

namespace Scanning.Services
{
    internal class RoomService: IRoomService, IInitializable
    {
        private readonly IPlaneMeshesProvider _planeMeshes;
        private readonly PlaneFactory _planeFactory;
        private readonly RoomConfigData _roomConfig;

        public RoomService(IPlaneMeshesProvider planeMeshes, RoomConfigData roomConfig)
        {
            _planeMeshes = planeMeshes;
            _roomConfig = roomConfig;
        }

        public void Initialize()
        {
            
        }

        public void CreateHoles()
        {
            foreach (var plane in _planeMeshes.PlaneMeshes)
            {
                _planeFactory.Create(_roomConfig.HolesMaterial, plane.Value);
            }
        }
    }
}