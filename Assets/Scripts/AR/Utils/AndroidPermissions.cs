using UnityEngine;
using UnityEngine.Android;
using Zenject;

namespace AR.Utils
{
    public class AndroidPermissions: IInitializable
    {
        internal void PermissionCallbacks_PermissionDeniedAndDontAskAgain(string permissionName)
        {
            Debug.Log($"{permissionName} PermissionDeniedAndDontAskAgain");
        }

        internal void PermissionCallbacks_PermissionGranted(string permissionName)
        {
            Debug.Log($"{permissionName} PermissionCallbacks_PermissionGranted");
        }

        internal void PermissionCallbacks_PermissionDenied(string permissionName)
        {
            Debug.Log($"{permissionName} PermissionCallbacks_PermissionDenied");
        }
        
        public void Initialize()
        {
            if (Permission.HasUserAuthorizedPermission(Permission.Camera))
            {
            }
            else
            {
                bool useCallbacks = false;
                if (!useCallbacks)
                {
                    Permission.RequestUserPermission(Permission.Camera);
                }
                else
                {
                    var callbacks = new PermissionCallbacks();
                    callbacks.PermissionDenied += PermissionCallbacks_PermissionDenied;
                    callbacks.PermissionGranted += PermissionCallbacks_PermissionGranted;
                    callbacks.PermissionDeniedAndDontAskAgain += PermissionCallbacks_PermissionDeniedAndDontAskAgain;
                    Permission.RequestUserPermission(Permission.Camera, callbacks);
                }
            }
        }
    }
}