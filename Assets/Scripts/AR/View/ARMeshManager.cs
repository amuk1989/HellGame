using System;
using UnityEngine;
using Zenject;

namespace AR.View
{
    internal class ARMeshManager: MonoBehaviour, IDisposable
    {
        internal class Factory: PlaceholderFactory<ARMeshManager>
        {
            
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}