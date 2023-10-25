using AR.Controllers;
using AR.Interfaces;
using Niantic.ARDK.AR;
using UnityEngine;

namespace AR.Services
{
    public class ARCameraService: ICameraProvider
    {
        private readonly ARController _controller;
        public Camera ARCamera => _controller.ARCamera;
        public Camera PortalCamera => _controller.PortalCamera;

        private ARCameraService(ARController controller)
        {
            _controller = controller;
        }

        public bool IsPointInView(Vector3 point)
        {
            var viewportPoint = _controller.ARCamera.WorldToViewportPoint(point);
            return (viewportPoint.z > 0 &&  (new Rect(0, 0, 1, 1)).Contains(viewportPoint));
        }

        public Vector3 CameraSightDirection => _controller.ARCamera.transform.forward;
        public Quaternion CameraRotation => _controller.ARCamera.transform.rotation;

        public Vector3 CameraPosition => _controller.ARCamera.transform.position;
    }
}