using AR.Models;
using UnityEngine;
using Zenject;

namespace AR.View
{
    public class AnchorDebugInfo : MonoBehaviour
    {
        public class Factory: PlaceholderFactory<PlaneModel, AnchorDebugInfo>
        {
        }

        private PlaneModel _model;
        
        
    }
}