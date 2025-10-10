using System;
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
    [Tooltip("Lock of the scale of the points")]
    public Vector3 lockScale = new Vector3(1, 1, 1);
    

    private void Lockpointscale()
    {
        for(int i = 0; i < bodyPoints.Count; i++)
        {

            bodyPoints[i].transform.localScale = lockScale;

        }
    }

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
