using AR.Interfaces;
using Scanning.Interfaces;
using UnityEngine.PlayerLoop;
using Zenject;

namespace Scanning.Services
{
    internal class RoomService: IRoomService, IInitializable
    {
        private readonly IARService _arService;

        public RoomService(IARService arService)
        {
            _arService = arService;
        }

        public void Initialize()
        {
            
        }
    }
}