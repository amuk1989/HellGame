using System;
using UnityEngine;

namespace AR.View
{
    public class AnchorView : MonoBehaviour, IDisposable
    {
        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}