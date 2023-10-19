using UnityEngine;

namespace AR.Interfaces
{
    public interface ICameraProvider
    {
        public Camera ARCamera { get; }
    }
}