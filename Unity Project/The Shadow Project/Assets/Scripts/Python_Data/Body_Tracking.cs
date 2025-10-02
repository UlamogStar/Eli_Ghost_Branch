using UnityEngine;
using System.Collections.Generic;
public class Body_Tracking : MonoBehaviour
{
    public UDP udpBody;
    public GameObject bodyPointPrefab; // Prefab to clone for each landmark
    private List<GameObject> bodyPoints = new List<GameObject>();
    
    // Adjust these to match your scene scale
    public float scale = 0.01f;
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
            GameObject newPoint = Instantiate(bodyPointPrefab);
            newPoint.transform.parent = transform; // Keep hierarchy clean
            bodyPoints.Add(newPoint);
        }

        // Update positions
        for (int i = 0; i < pointNumber; i++)
        {
            float x = float.Parse(points[i * 2]) * scale + offset.x;
            float y = float.Parse(points[i * 2 + 1]) * scale + offset.y;
            float z = offset.z;

            bodyPoints[i].transform.localPosition = new Vector3(x, y, z);
        }
    }
}