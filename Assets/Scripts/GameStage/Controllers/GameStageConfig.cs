using GameStage.Data;
using UnityEngine;

namespace GameStage.Controllers
{
    [CreateAssetMenu(fileName = "GameStageConfig", menuName = "Configs/GameStageConfig", order = 0)]
    public class GameStageConfig : ScriptableObject
    {
        [SerializeField] private GameStageId[] _gameStageIds;
        
        public GameStageId[] GameStageIds => _gameStageIds;
    }
}