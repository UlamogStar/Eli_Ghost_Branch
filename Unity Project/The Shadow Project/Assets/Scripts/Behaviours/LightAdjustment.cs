using UnityEngine;

public class LightAdjustment : MonoBehaviour
{
    private float light;
    private Light adjustLight;
    void Awake()
    {
        light = this.GetComponent<Light>().intensity;
        adjustLight = this.GetComponent<Light>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            light += 10;
            adjustLight.intensity = light;
        }
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            light -= 10;
            adjustLight.intensity = light;
        }
    }
}
