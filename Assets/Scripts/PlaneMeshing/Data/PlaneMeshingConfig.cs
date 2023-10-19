using UnityEngine;

namespace PlaneMeshing.Data
{
    [CreateAssetMenu(fileName = "PlaneMeshingConfig", menuName = "Configs/PlaneMeshingConfig", order = 0)]
    public class PlaneMeshingConfig : ScriptableObject
    {
        [Range(0,0.5f)][SerializeField] private float _antialiasingTrashHold;

        public float AntialiasingTrashHold => _antialiasingTrashHold;
    }
}