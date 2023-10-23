using UnityEngine;

namespace AR.Interfaces
{
    public interface ICameraProvider
    {
        public Camera ARCamera { get; }
        public bool IsPointInView(Vector3 point);
        public Vector3 CameraSightDirection { get; }
        public Vector3 CameraPosition { get; }
        public Quaternion CameraRotation { get; }
    }
}