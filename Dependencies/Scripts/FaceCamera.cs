using UnityEngine;

namespace BpmnElements
{

    /// <summary>
    /// This script makes the object it is attached to face the camera. 
    /// It is used by the billboards in of the SequenceFlows.
    /// Uncomment the lines with OVRCameraRig if you are using a VR/AR setup.
    /// </summary>
    public class FaceCamera : MonoBehaviour
    {
        //private OVRCameraRig _cameraRig;
        private Camera _camera;
        // Start is called before the first frame update
        void Start()
        {
            // Find the camera rig in the scene
            //_cameraRig = FindObjectOfType<OVRCameraRig>();
            _camera = FindObjectOfType<Camera>();
        }

        // Update is called once per frame
        void LateUpdate()
        {
            //if (_cameraRig == null)
            //{
            //    return;
            //}
            //Vector3 cameraPosition = _cameraRig.centerEyeAnchor.transform.position;
            Vector3 cameraPosition = _camera.transform.position;
            transform.LookAt(cameraPosition);
        }
    }
}
