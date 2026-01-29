using UnityEngine;

public class RestPostion : MonoBehaviour
{
    [TextArea(3, 10)]
    public string midpointNotes =
        "This script handles the midpoint of the tracked body. " +
        "It reads midpoint data from UDPReceive, updates the midpoint object, " +
        "and if the midpoint data stops updating, the midpoint will be moved to a fallback location.";

    [Tooltip("Reference to the UDPReceive script providing midpoint data.")]
    public UDP udpReceive;

    [Tooltip("Object that represents the midpoint in Unity.")]
    public GameObject midpointObject;
    
    [Tooltip("How long old data is allowed before snapping midpoint to fallback.")]
    public float updateThreshold = 1f;

    private string previousData = "";
    private float timeSinceLastUpdate = 0f;

    [Tooltip("Position the midpoint goes to when data stops updating.")]
    public Vector3 fallbackPosition = new Vector3(0, -12f, -10f);

    public SkinnedMeshRenderer MeshRenderer;

    void FixedUpdate()
    {
        string data = udpReceive.data;

        // If the midpoint data string is empty, do nothing
        if (string.IsNullOrEmpty(data))
            return;

        // Check if new midpoint data arrived
        if (data != previousData)
        {
            previousData = data;
            timeSinceLastUpdate = 0f;
            MeshRenderer.enabled = true;
            

        }
        else
        {
            // No change in data: count time
            timeSinceLastUpdate += Time.deltaTime;

            if (timeSinceLastUpdate > updateThreshold & udpReceive.data[13] != '0')
            {
                MeshRenderer.enabled = false;
            }
        }
    }
}

