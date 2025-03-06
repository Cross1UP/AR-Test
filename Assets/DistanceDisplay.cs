using UnityEngine;
using UnityEngine.UI;  

public class DistanceDisplay : MonoBehaviour
{
    public GameObject targetCube;
    public Text distanceText; 

    void Update()
    {
        if (targetCube != null && distanceText != null)
        {
            float distance = Vector3.Distance(Camera.main.transform.position, targetCube.transform.position);

            distanceText.text = "Distance: " + distance.ToString("F2") + " meters";
        }
    }
}