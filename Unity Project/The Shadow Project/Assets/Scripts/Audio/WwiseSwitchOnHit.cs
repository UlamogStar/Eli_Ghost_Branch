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

        SetItemType itemData = other.GetComponentInParent<SetItemType>();
        GameObject hitObject = itemData != null ? itemData.gameObject : other.gameObject;


        

        //apply switches
        AK.Wwise.Switch typeSwitch = itemData != null && itemData.itemType != null ? itemData.itemType : defaultItemType;
        AK.Wwise.Switch sizeSwitch = itemData != null && itemData.itemSize != null ? itemData.itemSize : defaultItemSize;



        // If we found SetItemType, use that object's GameObject as the emitter
        GameObject emitter = itemData != null ? itemData.gameObject : audioEmitter ?? gameObject;

        typeSwitch?.SetValue(audioEmitter);
        sizeSwitch?.SetValue(audioEmitter);



        //play the event
        impactEvent?.Post(audioEmitter);

        Debug.Log(itemData != null
        ? $"[WwiseSwitchOnHit] Found SetItemType on {itemData.gameObject.name}, applied {itemData.itemType?.Name} / {itemData.itemSize?.Name}"
        : $"[WwiseSwitchOnHit] No SetItemType found on {other.name} or its parents — using defaults.");
    }
}
