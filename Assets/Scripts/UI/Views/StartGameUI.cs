using UnityEngine;
using Zenject;

namespace UI.Views
{
    public class StartGameUI: MonoBehaviour
    {
        public class Factory: PlaceholderFactory<Transform, StartGameUI>
        {
        }
    }
}