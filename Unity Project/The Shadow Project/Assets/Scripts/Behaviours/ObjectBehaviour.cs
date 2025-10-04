/*
Originial Coder: Owynn A.
Recent Coder: Zackery E.
Recent Changes: Added variables and headers
Last date worked on: 9/29/2025
*/

using UnityEngine;

public class ObjectBehaviour : MonoBehaviour
{
    [Header("Settings")]
    public IntData damage;
    public float throwSpeed = 1f;
    public bool returnable = false, thrown = false;
	[SerializeField] private ThrowObjectBehavior throwManager;
	[SerializeField] private Transform target;

    [Header("Info")]
    public ObjectSpawner originSpawn;
    public Transform returnToObject; // Object to return to (Ghost)

    public void Awake()
    {
	    GameObject obj = GameObject.FindGameObjectWithTag("Ghost");
	    target = obj.transform;
    }
    public void OnReturn()
    {
	    Vector3 location = target.position;
	    throwManager.StartThrow(this.gameObject, location, throwSpeed);
    }

}// end class