using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;

public class ARGrabRelease : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Camera arCamera;
    public float interactionRange = 1.0f;
    
    private Transform cubeTransform;
    private float distanceFromCamera; // Store this as a class variable
    private bool isButtonHeld = false;
    private bool isHoldingCube = false;
    
    void Update()
    {
        if (isButtonHeld)
        {
            if (!isHoldingCube)
            {
                Ray ray = arCamera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, interactionRange))
                {
                    if (hit.collider.CompareTag("Cube")) // Ensure cubes have the "Cube" tag
                    {
                        cubeTransform = hit.transform;
                        distanceFromCamera = Vector3.Distance(cubeTransform.position, arCamera.transform.position);
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
                float currentDistance = Vector3.Distance(arCamera.transform.position, cubeTransform.position);
                if (currentDistance > interactionRange)
                {
                    isHoldingCube = false;
                    cubeTransform = null;
                    Handheld.Vibrate(); // Vibrate when cube is out of range
                }
                else
                {
                    cubeTransform.position = arCamera.transform.position + arCamera.transform.forward * distanceFromCamera;
                    cubeTransform.rotation = arCamera.transform.rotation;
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