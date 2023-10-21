using UnityEngine;
using Zenject;

namespace UI.Views
{
    public abstract class BaseUI : MonoBehaviour
    {
        public sealed class Factory: PlaceholderFactory<string, Transform, BaseUI>
        {
        }
    }
}