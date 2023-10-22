using System;
using GameStage.Data;

namespace GameStage.Interfaces
{
    public interface IGameStageService
    {
        public IObservable<GameStageId> GameStageAsObservable();
        public void NextStage();
    }
}