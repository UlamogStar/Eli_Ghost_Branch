using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
public class LoadPlayer : MonoBehaviour
{
    public UnityEvent OnLoadPlayer, OnUnloadPlayer;
    public bool wait = false;

    public void OnTriggerExit(Collider other)
    {
        StartCoroutine(RemovePlayer(other));
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("JoinTrigger"))
        {
            OnLoadPlayer.Invoke();
            wait = true;
        }
        
    }

    public IEnumerator RemovePlayer(Collider other)
    {
        wait = false;
        yield return new WaitForSeconds(1f);
        if (wait == false)
        {
            if (other.CompareTag("JoinTrigger"))
            {
                OnUnloadPlayer.Invoke();
            }
        }
    }
}
