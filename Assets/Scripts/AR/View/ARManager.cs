using UnityEngine;
using Zenject;

namespace AR.View
{
    internal class ARManager : MonoBehaviour
    {
        internal class ARFactory: PlaceholderFactory<ARManager>
        {
        }
    }
}