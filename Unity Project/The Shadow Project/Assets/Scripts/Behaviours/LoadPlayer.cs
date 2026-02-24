using UnityEngine;
using UnityEngine.Events;

public class LoadPlayer : MonoBehaviour
{
    public UnityEvent OnLoadPlayer, OnUnloadPlayer;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("JoinTrigger"))
        {
            OnLoadPlayer.Invoke();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("JoinTrigger"))
        {
            OnUnloadPlayer.Invoke();
        }
    }
}
