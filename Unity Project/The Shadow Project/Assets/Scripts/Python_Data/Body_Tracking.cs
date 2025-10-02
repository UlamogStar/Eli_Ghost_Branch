using UnityEngine;
using System.Collections.Generic;
public class Body_Tracking : MonoBehaviour
{
    public UDP udpBody;
    public GameObject bodyPoint; // Single GameObject to move
    [Tooltip("in case not ports are assigned it will use these th make point and carte a skeleton for testing")]
    public GameObject bodyPointPrefab; // Prefab to clone for each landmark
    [Tooltip("Assign point in order of Left arm 1-3 , right arm 1-3, left leg 1-3, right leg 1-3, then last is mid point")]
    public List<GameObject> bodyPoints = new List<GameObject>();
    
    // Adjust these to match your scene scale
    public float scale = 0.01f;
    [Tooltip("deaflut is (X = -7, Y = 12, Z = 0)")]
    public Vector3 offset = new Vector3(0, 0, 0);

    private void FixedUpdate()
    {
        string data = udpBody.data;

        if (string.IsNullOrEmpty(data))
            return;

        data = data.Trim(new char[] { '[', ']' });
        string[] points = data.Split(',');

        int pointNumber = points.Length / 2; // assuming data is x,y,z

        // Create missing points
        while (bodyPoints.Count < pointNumber)
        {
            Debug.LogError("Not enough points received.");
            return;
            GameObject newPoint = Instantiate(bodyPointPrefab);
            // newPoint.transform.parent = transform; // Keep hierarchy clean
            bodyPoints.Add(newPoint);
        }

        // Parse the first landmark position
        // x-xis is minused by 7 because it offenses the position of the body by 7 when transferred
        float x = 7 - float.Parse(points[0]) / 100;
        float y = float.Parse(points[1]) / 100;
        
        // Update the position of the single GameObject
        bodyPoint.transform.localPosition = new Vector3(x, y, bodyPoint.transform.localPosition.z);
        // Update positions
        for (int i = 0; i < pointNumber; i++)
        {
            float x = float.Parse(points[i * 2]) * scale + offset.x;
            float y = float.Parse(points[i * 2 + 1]) * scale + offset.y;
            float z = offset.z;

            Vector3 targetPos = new Vector3(x, y, z);
            
            bodyPoints[i].transform.localPosition = new Vector3(x, y, z);
            
            /*
            // Smoothly interpolate from current to target
            bodyPoints[i].transform.localPosition = Vector3.Lerp(
                bodyPoints[i].transform.localPosition,
                targetPos,
                1f // <-- smoothing factor, tweak between 0.05f (very smooth) and 1.0f (instant snap)
            );
            */
        }
    }
}