using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        if (target == null) return;

        Vector3 dir = target.position - transform.position;

        if (dir.sqrMagnitude < 0.0001f) return;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // Fix: Add 180 degrees when pointing left
        if (dir.x < 0)
        {
            angle += 180f;
        }

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}