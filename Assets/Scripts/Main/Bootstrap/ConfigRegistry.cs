using GameStage.Controllers;
using PlaneMeshing.Data;
using UnityEngine;
using Utility;
using Zenject;

namespace Main.Bootstrap
{
    [CreateAssetMenu(fileName = "ConfigRegistry", menuName = "Registries/ConfigRegistry", order = 0)]
    public class ConfigRegistry : ScriptableObjectInstaller
    {
        [SerializeField] private PlaneMeshingConfig _planeMeshingConfig;
        [SerializeField] private GameStageConfig _gameStageConfig;

        public override void InstallBindings()
        {
            Container.InstallRegistry(_planeMeshingConfig);
            Container.InstallRegistry(_gameStageConfig);
        }
    }
}