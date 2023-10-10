using AR.View;
using UnityEngine;

namespace AR.Data
{
    [CreateAssetMenu(fileName = "ARDebugConfig", menuName = "Debug configs/ARDebugConfig", order = 0)]
    public class ARDebugConfig : ScriptableObject
    {
        [SerializeField] private AnchorView _anchorView;

        public AnchorView AnchorView => _anchorView;
    }
}