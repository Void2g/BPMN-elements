using System.Linq;
using TMPro;
using UnityEngine;

namespace BpmnElements
{
    /// <summary>
    /// This script creates a line between two GameObjects.
    /// It uses the markers of the BPMN elements to determine the start and end of the line.
    /// The line is created with two intermediate points to make it look similar to 2D BPMN modeling.
    /// It positions the arrowhead at the end of the line and the label in the middle of the line.
    /// </summary>
    public class LineCreation : MonoBehaviour
    {
        public GameObject startObject; // Reference to the first GameObject
        public GameObject endObject;   // Reference to the second GameObject

        public GameObject arrowHead;
        public LineRenderer lineRenderer;

        public GameObject label;

        public float LineWidth = 0.01f;

        private Vector3[] lastposions = new Vector3[2];

        void Start()
        {
            lineRenderer.startColor = Color.black;
            lineRenderer.endColor = Color.black;
            lineRenderer.startWidth = LineWidth;
            lineRenderer.endWidth = LineWidth;

            //If TextmeshPro in label has empty text deactivate gameobject
            TextMeshPro textLabel = label.GetComponentInChildren<TextMeshPro>();
            if (string.IsNullOrEmpty(textLabel.text))
            {
                label.SetActive(false);
            }
            UpdateLine(lineRenderer, startObject, endObject);
        }

        void Update()
        {
            //if last positions are not the same as the current positions, update the line
            if (lastposions[0] != startObject.transform.position || lastposions[1] != endObject.transform.position)
            {
                UpdateLine(lineRenderer, startObject, endObject);
                lastposions[0] = startObject.transform.position;
                lastposions[1] = endObject.transform.position;
            }
        }

        private void UpdateLine(LineRenderer l, GameObject a, GameObject b)
        {

            Transform markersParentA = a.transform.Find("Markers");
            Transform markersParentB = b.transform.Find("Markers");
            Transform[] aMarkers = markersParentA.GetComponentsInChildren<Transform>();
            Transform[] bMarkers = markersParentB.GetComponentsInChildren<Transform>();

            // Remove the parent from the array
            aMarkers = aMarkers.Skip(1).ToArray();
            bMarkers = bMarkers.Skip(1).ToArray();

            // Find the closest marker pair between the two elements to draw the line between
            Transform closestA = null;
            Transform closestB = null;
            float minDistance = float.MaxValue;
            foreach (var markerA in aMarkers)
            {
                foreach (var markerB in bMarkers)
                {
                    float distance = Vector3.Distance(markerA.transform.position, markerB.transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closestA = markerA;
                        closestB = markerB;
                    }
                }
            }
            Vector3 start = closestA.position;
            Vector3 end = closestB.position;


            // Compute the direction of the markers as this should be the initial direction of the line
            Vector3 startCenter = a.transform.position;
            Vector3 endCenter = b.transform.position;

            Vector3 startDirection = (start - startCenter).normalized;
            Vector3 endDirection = (end - endCenter).normalized;


            // Compute the midpoint between the two markers
            Vector3 midPoint = (start + end) * 0.5f;

            // Compute how far along the marker's normal the midpoint is.
            // This is done by projecting the vector (midPoint - marker) onto the marker's normal.
            float offsetDistanceStart = Vector3.Dot(midPoint - start, startDirection);
            float offsetDistanceEnd = Vector3.Dot(midPoint - end, endDirection);

            // Ensure the distance is not negative so the intermediate points are not inside the elements
            // (as the line should not be drawn inside the elements if the distance between the cubes in the initial direction is negative)
            float minOffset = 0.1f;
            if (offsetDistanceStart < minOffset)
            {
                offsetDistanceStart = minOffset;
            }
            if (offsetDistanceEnd < minOffset)
            {
                offsetDistanceEnd = minOffset;
            }

            // Determine the intermediate points by moving from each marker along its normal by the computed distance
            Vector3 intermediate1 = start + startDirection * offsetDistanceStart;
            Vector3 intermediate2 = end + endDirection * offsetDistanceEnd;

            // Adjust the end point so the line stops inside the arrowhead (instead of at the tip which would make the line stick out)
            float arrowInsetDistance = 0.02f;
            Vector3 adjustedEnd = end + endDirection * arrowInsetDistance;


            // Update the LineRenderer with 4 positions
            l.positionCount = 4;
            l.SetPosition(0, start);
            l.SetPosition(1, intermediate1);
            l.SetPosition(2, intermediate2);
            l.SetPosition(3, adjustedEnd);

            //bring arrowhead to end position and rotate it to look at the target
            arrowHead.transform.position = end;
            arrowHead.transform.LookAt(b.transform);

            // Set label position to midpoint but slightly above it.
            label.transform.position = midPoint + new Vector3(0, 0.05f, 0);
        }
    }
}