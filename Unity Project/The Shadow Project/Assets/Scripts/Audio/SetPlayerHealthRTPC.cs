using UnityEngine;
using UnityEngine.UI;

public class SetPlayerHealthRTPC : MonoBehaviour
{
    [Header("Wwise RTPC Connection")]

    public AK.Wwise.RTPC playerHealthRTPC;
    public Slider playerHealthSlider;

    void Update()
    {
        playerHealthRTPC.SetGlobalValue(playerHealthSlider.value);
    }
}
