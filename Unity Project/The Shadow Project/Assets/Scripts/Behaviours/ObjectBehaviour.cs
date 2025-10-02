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
    public int throwSpeed = 10;
    public bool returnable = false;

    [Header("Info")]
    public ObjectSpawner originSpawn;
    public Transform returnToObject; // Object to return to (Ghost)

}// end class