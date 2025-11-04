using UnityEngine;

public class ThrowableReturnBehavior : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField] private Transform target;

    [Header("Components")]
    [SerializeField] private ThrowObjectBehavior throwObjectManager;

    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ObjectBehaviour objectBehaviour))
        {
            if (objectBehaviour.thrown == true && objectBehaviour.returnable == true)
            {
                // End Current Throw
                throwObjectManager.EndThrow(objectBehaviour.gameObject);

                // Start Return Throw
                objectBehaviour.returned = true;
                throwObjectManager.StartThrow(objectBehaviour.gameObject, target.position, objectBehaviour.throwSpeed);
            }
        }
    }


}
