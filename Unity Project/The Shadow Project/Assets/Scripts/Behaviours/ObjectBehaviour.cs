/*
Originial Coder: Owynn A.
Recent Coder: Zackery E.
Recent Changes: Added variables and headers
Last date worked on: 10/28/2025
*/

using UnityEngine;

public class ObjectBehaviour : MonoBehaviour
{
    [Header("Settings")]
    public IntData damage;
    public float throwSpeed = 1f;
    public ObjectSize size = ObjectSize.Small;
    public bool returnable = false, thrown = false;

    [Header("Info")]
    public ObjectSpawner originSpawn;
    public Transform returnToObject; // Object to return to (Ghost)

}



// Object sizes must be from small -> large
public enum ObjectSize
{
    Small, // 0
    Big // 1
}