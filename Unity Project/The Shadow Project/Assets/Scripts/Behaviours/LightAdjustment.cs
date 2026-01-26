using UnityEngine;

public class LightAdjustment : MonoBehaviour
{
    private float intensityValue;
    private Light adjustLight;

    void Awake()
    {
        adjustLight = GetComponent<Light>();
        if (adjustLight != null) intensityValue = adjustLight.intensity;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            ChangeIntensity(10f);
        }
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            ChangeIntensity(-10f);
        }
    }

    //Added by Elijah: Public methods for UI Buttons
    public void IncreaseIntensity()
    {
        ChangeIntensity(10f);
    }

    public void DecreaseIntensity()
    {
        ChangeIntensity(-10f);
    }

    private void SetIntensity(float value)
    {
        intensityValue = value;
        if (adjustLight == null) adjustLight = GetComponent<Light>();
        if (adjustLight != null) adjustLight.intensity = intensityValue;
    }

    private void ChangeIntensity(float delta)
    {
        intensityValue += delta;
        if (adjustLight == null) adjustLight = GetComponent<Light>();
        if (adjustLight != null) adjustLight.intensity = intensityValue;
    }
}
