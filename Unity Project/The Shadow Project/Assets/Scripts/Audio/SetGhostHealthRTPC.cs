using UnityEngine;
using UnityEngine.UI;

public class SetGhostHealthRTPC : MonoBehaviour
{
    [Header("Wwise RTPC Connection")]

    public AK.Wwise.RTPC ghostHealthRTPC;
    public Slider ghostHealthSlider;

    void Update()
    {
        ghostHealthRTPC.SetGlobalValue(ghostHealthSlider.value);
    }
}
