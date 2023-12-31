﻿using System;
using Base.Data;
using Base.Interfaces;
using GameStage.Data;
using UnityEngine;

namespace GameStage.Controllers
{
    [CreateAssetMenu(fileName = "GameStageConfig", menuName = "Configs/GameStageConfig", order = 0)]
    public class GameStageConfig : BaseConfig<GameStageConfigData>
    {
    }

    [Serializable]
    public struct GameStageConfigData: IConfigData
    {
        [SerializeField] private GameStageId[] _gameStageIds;
        
        public GameStageId[] GameStageIds => _gameStageIds;
    }
}