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
    public ObjectSize size = ObjectSize.Small;
    public IntData damage;
    public float throwSpeed = 1f;
    public bool returnable = false, thrown = false;

    [Header("Info")]
    public ObjectSpawner originSpawn;

}

// Object sizes must be from small -> large
public enum ObjectSize
{
    Small, // 0
    Big // 1
}