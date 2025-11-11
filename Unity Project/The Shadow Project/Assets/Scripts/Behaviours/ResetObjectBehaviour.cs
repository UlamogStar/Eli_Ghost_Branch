using UnityEngine;
using System.Collections;

public class ResetObjectBehaviour : MonoBehaviour
{
    public void StartTimeDestroy()
    {
        StartCoroutine(TimeDestroy());
    }
    IEnumerator TimeDestroy()
    {
        yield return new WaitForSeconds(4.0f);
        Reset();

    }
    public void Reset()
    {
        GameObject manager = GameObject.Find("ObjectManager");
        ObjectManager objectManager = manager.GetComponent<ObjectManager>();
        ObjectBehaviour objectBehaviour = this.gameObject.GetComponent<ObjectBehaviour>();
        objectManager.enableSpawnByObject(objectBehaviour);
        objectManager.spawnRandom(1);
        Destroy(this.gameObject);

    }
}
