using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public GameObject settingsPanel;
    public Text uiText;
    public Slider fontSizeSlider;
    public Slider colorSlider; 

    private bool isSettingsOpen = false;

    void Start()
    {
        settingsPanel.SetActive(false);

        fontSizeSlider.onValueChanged.AddListener(UpdateFontSize);
        colorSlider.onValueChanged.AddListener(UpdateFontColor);
    }

    public void OpenSettings()
    {
        isSettingsOpen = true;
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        isSettingsOpen = false;
        settingsPanel.SetActive(false);
    }

    void UpdateFontSize(float value)
    {
        uiText.fontSize = Mathf.RoundToInt(value);
    }

    void UpdateFontColor(float value)
    {
        uiText.color = Color.Lerp(Color.white, Color.red, value);
    }

    void Update()
    {
        // Detect touch input to close settings when tapping outside
        if (isSettingsOpen && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Vector2 touchPosition = touch.position;

                // Check if touch is outside the settings panel
                if (!RectTransformUtility.RectangleContainsScreenPoint(settingsPanel.GetComponent<RectTransform>(), touchPosition))
                {
                    CloseSettings();
                }
            }
        }
    }
}
