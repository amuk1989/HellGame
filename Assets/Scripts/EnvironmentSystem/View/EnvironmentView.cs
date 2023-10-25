using UnityEngine;
using Zenject;

namespace EnvironmentSystem.View
{
    public class EnvironmentView : MonoBehaviour
    {
        public class Factory: PlaceholderFactory<Vector3, Quaternion, EnvironmentView>
        {
        }

        [Inject]
        private void Construct(Vector3 position, Quaternion rotation)
        {
            transform.SetPositionAndRotation(position, rotation);
        }
    }
}