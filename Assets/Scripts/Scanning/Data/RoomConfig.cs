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

        public Material HolesMaterial => _holesMaterial;
    }
}