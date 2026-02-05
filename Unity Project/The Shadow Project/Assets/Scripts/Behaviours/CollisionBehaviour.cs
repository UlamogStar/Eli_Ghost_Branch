using UnityEngine;
using UnityEngine.Events;

public class CollisionBehaviour : MonoBehaviour
{
    public UnityEvent onCollisionEnter;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            GhostBehaviour ghost = other.GetComponentInParent<GhostBehaviour>();
            ghost.waiting = false;
            ghost.StartCoroutine(ghost.Throw());

        }
        
        onCollisionEnter.Invoke();
    }
}
