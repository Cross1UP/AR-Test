using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CameraAccessM : MonoBehaviour
{
    private ARCameraManager arCameraManager;

    private bool isCameraEnabled = false;

    void Start()
    {

        arCameraManager = GetComponent<ARCameraManager>();
        
        if (arCameraManager != null)
        {
            EnableCamera();
        }
        else
        {
            Debug.LogError("ARCameraManager component not found on the AR Camera.");
        }
    }

    void Update()
    {

        if (!isCameraEnabled && arCameraManager.subsystem != null)
        {
            EnableCamera();
        }
    }

    void EnableCamera()
    {
        if (arCameraManager.subsystem != null && !isCameraEnabled)
        {
            arCameraManager.enabled = true; 
            isCameraEnabled = true;
            Debug.Log("Camera active.");
        }
    }


    public void DisableCamera()
    {
        if (arCameraManager.subsystem != null && isCameraEnabled)
        {
            arCameraManager.enabled = false; 
            isCameraEnabled = false;
            Debug.Log("Camera not active.");
        }
    }
}