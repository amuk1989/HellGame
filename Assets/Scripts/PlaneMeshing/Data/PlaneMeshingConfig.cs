using System;
using Base.Data;
using Base.Interfaces;
using UnityEngine;

namespace PlaneMeshing.Data
{
    [CreateAssetMenu(fileName = "PlaneMeshingConfig", menuName = "Configs/PlaneMeshingConfig", order = 0)]
    public class PlaneMeshingConfig : BaseConfig<PlaneMeshingConfigData>
    {
    }

    [Serializable]
    public struct PlaneMeshingConfigData: IConfigData
    {
        [Range(0,0.5f)][SerializeField] private float _antialiasingTrashHold;
        [SerializeField] private Material _planeMaterial;

        public float AntialiasingTrashHold => _antialiasingTrashHold;
        public Material PlaneMaterial => _planeMaterial;
    }
}