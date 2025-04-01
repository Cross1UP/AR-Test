using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;

public class ARGrabRelease : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Camera arCamera;
    public float interactionRange = 1.0f;

    private Transform cubeTransform;
    private Vector3 offset;
    private bool isButtonHeld = false;
    private bool isHoldingCube = false;

    void Update()
    {
        if (isButtonHeld)
        {
            if (!isHoldingCube)
            {
                Ray ray = arCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, interactionRange))
                {
                    if (hit.collider.CompareTag("Cube")) // Ensure cubes have the "Cube" tag
                    {
                        cubeTransform = hit.transform;
                        offset = cubeTransform.position - arCamera.transform.position;
                        isHoldingCube = true;
                    }
                }
                else
                {
                    Handheld.Vibrate(); // Vibrate if no cube is detected
                }
            }

            if (isHoldingCube)
            {
                float distanceToCube = Vector3.Distance(arCamera.transform.position, cubeTransform.position);
                if (distanceToCube > interactionRange)
                {
                    isHoldingCube = false;
                    cubeTransform = null;
                    Handheld.Vibrate(); // Vibrate when cube is out of range
                }
                else
                {
                    cubeTransform.position = arCamera.transform.position + offset;
                }
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isButtonHeld = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isButtonHeld = false;
        isHoldingCube = false;
    }
}
