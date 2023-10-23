using UnityEngine;

namespace Utility
{
    public static class Consts
    {
        public const string MainCanvas = "Prefabs/UI/MainCanvas";
        public const string StartUi = "Prefabs/UI/StartUI";
        public const string ARManager = "Prefabs/ARSceneManager";
        public const string ARMesh = "Prefabs/ARMesh";
        public const string Plane = "Prefabs/Environment/PlaneView";
        public const string ScanProgressUI = "Prefabs/UI/ScannedProgressUI";
        public const string ScanStopUI = "Prefabs/UI/StopScanningUI";
        public const string Environment = "Prefabs/Environment/Slender";
        public static readonly LayerMask DefaultLayer = LayerMask.NameToLayer("Default");
    }
}