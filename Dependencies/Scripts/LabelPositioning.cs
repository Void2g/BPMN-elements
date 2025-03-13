using TMPro;
using UnityEngine;

namespace BpmnElements
{
    /// <summary>
    /// This script makes the label object face the camera
    /// and also determines if it should be placed above or below the BPMN element.
    /// It is used by the billboards of the BPMN elements.
    /// Uncomment the lines with OVRCameraRig if you are using a VR/AR setup.
    /// </summary>
    public class LabelPositioning : MonoBehaviour
    {
        //private OVRCameraRig _cameraRig;
        private Camera _camera;
        public GameObject label;
        public Transform element;
        private float distance = 0.9f;

        void Start()
        {
            // Find the camera rig in the scene
            //_cameraRig = FindObjectOfType<OVRCameraRig>();
            _camera = FindObjectOfType<Camera>();

            //If TextmeshPro in label has empty text deactivate gameobject
            TextMeshPro textLabel = label.GetComponentInChildren<TextMeshPro>();
            if (string.IsNullOrEmpty(textLabel.text))
            {
                label.SetActive(false);
            }
        }

        void LateUpdate()
        {
            //if (_cameraRig == null)
            //{
            //    return;
            //}

            Vector3 cameraPosition = _camera.transform.position;
            //Vector3 cameraPosition = _cameraRig.centerEyeAnchor.transform.position;
            // set label to face the camera
            label.transform.LookAt(cameraPosition);

            // determine if the view onto the label is blocked by the element -> check if camera is below the element
            if (element.position.y > cameraPosition.y)
            {
                // move the label to the bottom of the element
                label.transform.localPosition = new Vector3(0, -distance, 0);
            }
            else
            {
                // move the label to the top of the element
                label.transform.localPosition = new Vector3(0, distance, 0);
            }

        }
    }
}