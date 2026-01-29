using UnityEngine;
using UnityEngine.Events;


public class SpawnListBehaviour : MonoBehaviour
{
    [SerializeField] public TransformDataList activePlayers;
    public UnityEvent onTrigger;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onTrigger.Invoke();
            TransformDataList list = other.GetComponent<TransformDataList>();
           activePlayers.posList.AddRange(list.posList);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TransformDataList list = other.GetComponent<TransformDataList>();
            foreach (Transform hitbox in list.posList)
            {
                activePlayers.posList.Remove(hitbox);
            }
        }
    }
}