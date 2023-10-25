using UnityEngine;
using Zenject;

namespace EnvironmentSystem.View
{
    public class EnvironmentView : MonoBehaviour
    {
        public class Factory: PlaceholderFactory<EnvironmentView>
        {
        }
    }
}