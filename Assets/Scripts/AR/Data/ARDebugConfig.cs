using AR.View;
using UnityEngine;

namespace AR.Data
{
    [CreateAssetMenu(fileName = "ARDebugConfig", menuName = "Debug configs/ARDebugConfig", order = 0)]
    public class ARDebugConfig : ScriptableObject
    {
        [SerializeField] private AnchorView _anchorView;
        [SerializeField] private AnchorDebugInfo _anchorDebugInfo;
        [SerializeField] private bool _isShownAnchorPlanes;
        [SerializeField] private bool _isShownAnchorInfo;

        public AnchorView AnchorView => _anchorView;
        public bool IsShownAnchorPlanes => _isShownAnchorPlanes;
        public bool IsShownAnchorInfo => _isShownAnchorInfo;
        public AnchorDebugInfo DebugInfo => _anchorDebugInfo;
    }
}