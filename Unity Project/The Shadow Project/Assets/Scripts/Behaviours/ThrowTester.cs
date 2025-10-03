using UnityEngine;

// Temp Test Script

public class ThrowTester : MonoBehaviour
{
    [SerializeField] private ThrowObjectBehavior throwManager;
    [SerializeField] private GameObject throwablePrefab;
    [SerializeField] private GameObject startObject;
    [SerializeField] private Transform target;
    [SerializeField] private float throwSpeed = 1f;
    [SerializeField] private float overshoot = 0.5f;

    private GameObject currentThrowable;

    void Update()
    {
        // Press SPACE to spawn + throw
        if (Input.GetKeyDown(KeyCode.Space))
        {

            Vector3 location = target.position;

            if (currentThrowable != null)
            {
                // Stop coroutine for this object
                throwManager.EndThrow(currentThrowable);

                // Destroy the object
                Destroy(currentThrowable);
                currentThrowable = null;
            }

            currentThrowable = Instantiate(throwablePrefab, startObject.transform.position, Quaternion.identity);

            throwManager.StartThrow(currentThrowable, location, throwSpeed);
        }
    }
}