using System;
using Base.Data;
using Base.Interfaces;
using UnityEngine;

namespace Scanning.Data
{
    [CreateAssetMenu(fileName = "RoomConfig", menuName = "Configs/RoomConfig", order = 0)]
    public class RoomConfig : BaseConfig<RoomConfigData>
    {
    }

    [Serializable]
    public struct RoomConfigData: IConfigData
    {
        [SerializeField] private Material _holesMaterial;
        [SerializeField] private float _minArea;
        [SerializeField] private LayerMask _stencilLayerMask;

        public Material HolesMaterial => _holesMaterial;
        public float MinArea => _minArea;
        public LayerMask StencilLayerMask => _stencilLayerMask;
    }
}