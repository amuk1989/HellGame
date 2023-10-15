using System;
using UnityEngine;

namespace AR.View
{
    public class AnchorView : MonoBehaviour, IDisposable
    {
        [SerializeField] private Transform _plane;
        public void SetSize(Vector3 size)
        {
            _plane.localScale = size + Vector3.up;
        }
        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}