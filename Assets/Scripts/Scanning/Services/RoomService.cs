using PlaneMeshing.Factories;
using PlaneMeshing.Interfaces;
using Scanning.Data;
using Scanning.Interfaces;
using Scanning.Repositories;
using Zenject;

namespace Scanning.Services
{
    internal class RoomService: IRoomService, IInitializable
    {
        private readonly IPlaneMeshesProvider _planeMeshes;
        private readonly HolesRepository _holesRepository;
        
        public RoomService(IPlaneMeshesProvider planeMeshes, HolesRepository holesRepository)
        {
            _planeMeshes = planeMeshes;
            _holesRepository = holesRepository;
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
    }
}