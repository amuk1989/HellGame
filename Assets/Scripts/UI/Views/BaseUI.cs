using System;
using UnityEngine;
using Zenject;

namespace UI.Views
{
    public abstract class BaseUI : MonoBehaviour, IDisposable
    {
        public sealed class Factory: PlaceholderFactory<string, Transform, BaseUI>
        {
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}