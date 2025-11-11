using UnityEngine;
using System.Collections.Generic;

public class Body_Tracking : MonoBehaviour
{
    public UDP udpBody;

    [System.Serializable]
    public class BodySet
    {
        public List<GameObject> points = new List<GameObject>(14); 
        // IMPORTANT: exactly 14 per set (13 joints + midpoint)
    }

    public List<BodySet> sets = new List<BodySet>();   // Expect 4 sets
    public float scale = 0.01f;
    public Vector3 offset;
    public float timeout = 0.5f;

    private float[] lastUpdate; 
    private Vector3[][] restPositions;

    void Start()
    {
        lastUpdate = new float[sets.Count];
        restPositions = new Vector3[sets.Count][];

        for (int s = 0; s < sets.Count; s++)
        {
            restPositions[s] = new Vector3[sets[s].points.Count];

            for (int i = 0; i < sets[s].points.Count; i++)
            {
                restPositions[s][i] = sets[s].points[i].transform.position;
            }
        }
    }

    void FixedUpdate()
    {
        string raw = udpBody.data;
        if (string.IsNullOrWhiteSpace(raw))
            return;

        string[] pts = raw.Split(',');
        int perSet = 14 * 2;  // 14 points * (x,y)

        if (pts.Length < perSet)
            return;

        for (int s = 0; s < sets.Count; s++)
        {
            int start = s * perSet;

            if (start + perSet > pts.Length)
                break;

            bool updated = false;

            for (int i = 0; i < sets[s].points.Count; i++)
            {
                int baseIndex = start + i * 2;

                bool okX = float.TryParse(pts[baseIndex], out float x);
                bool okY = float.TryParse(pts[baseIndex + 1], out float y);

                if (!okX || !okY)
                    continue;

                // If Python sent zeros → no person found → use rest pose
                if (Mathf.Abs(x) < 0.001f && Mathf.Abs(y) < 0.001f)
                {
                    sets[s].points[i].transform.position = restPositions[s][i];
                    continue;
                }

                Vector3 pos = new Vector3(
                    -x * scale + offset.x,
                    y * scale + offset.y,
                    offset.z
                );

                sets[s].points[i].transform.position = pos;
                updated = true;
            }

            if (updated)
                lastUpdate[s] = Time.time;
        }

        ApplyTimeouts();
    }

    void ApplyTimeouts()
    {
        for (int s = 0; s < sets.Count; s++)
        {
            if (Time.time - lastUpdate[s] > timeout)
            {
                for (int i = 0; i < sets[s].points.Count; i++)
                {
                    sets[s].points[i].transform.position = restPositions[s][i];
                }
            }
        }
    }
}