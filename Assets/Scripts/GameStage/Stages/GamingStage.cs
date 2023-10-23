using GameStage.Interfaces;
using Scanning.Interfaces;

namespace GameStage.Stages
{
    internal class GamingStage: IGameStage
    {
        private readonly IRoomService _roomService;

        public GamingStage(IRoomService roomService)
        {
            _roomService = roomService;
        }

        public void Execute()
        {
            _roomService.CreateHoles();
            _roomService.CreateEnvironment();
        }

        public void Complete()
        {
            
        }
    }
}