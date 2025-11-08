using UnityEngine;

public class ActivateBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnTriggerEnter(Collider other)
    {
        ObjectBehaviour objectBehaviour = other.GetComponent<ObjectBehaviour>();
        objectBehaviour.enabled = true;
    }
}
