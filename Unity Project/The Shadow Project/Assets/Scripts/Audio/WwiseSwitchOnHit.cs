using UnityEngine;

public class WwiseSwitchOnHit : MonoBehaviour
{
    [Header("Audio Settings")]
    public GameObject audioEmitter;         //where should the sound come from?
    public AK.Wwise.Switch defaultItemType;
    public AK.Wwise.Switch defaultItemSize;
    public AK.Wwise.Event impactEvent;      //the Wwise event to play

    private void OnTriggerEnter(Collider other)
    {
        //only care about your throwable objects
        if (!other.CompareTag("Object"))
            return;

        GameObject hitObject = other.transform.root.gameObject; //in case collider is child
        SetItemType itemData = hitObject.GetComponent<SetItemType>();

        //apply switches
        AK.Wwise.Switch typeSwitch = itemData != null && itemData.itemType != null ? itemData.itemType : defaultItemType;
        AK.Wwise.Switch sizeSwitch = itemData != null && itemData.itemSize != null ? itemData.itemSize : defaultItemSize;

        if (audioEmitter == null)
            audioEmitter = gameObject;

        typeSwitch?.SetValue(audioEmitter);
        sizeSwitch?.SetValue(audioEmitter);



        //play the event
        impactEvent?.Post(audioEmitter);

        Debug.Log($"Applied switches from {hitObject.name} and played event {impactEvent}");
    }
}
