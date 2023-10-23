using UnityEngine;
using Zenject;

namespace Scanning.View
{
    public class Environment : MonoBehaviour
    {
        public class Factory: PlaceholderFactory<Environment>
        {
        }
    }
}