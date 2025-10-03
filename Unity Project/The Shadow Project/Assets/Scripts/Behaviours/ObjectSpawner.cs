/*
Originial Coder: Zackery E.
Recent Coder: Zackery E.
Recent Changes: Initial Coding
Last date worked on: 9/25/2025
*/

using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public ObjectListSO spawnables;
    public Transform spawnLocation;

    public void Start()
    {
        spawnLocation = gameObject.transform;
    }

    public GameObject GetRandomObject()
    {
        int randomSpawnableIndex = Random.Range(0, spawnables.list.Count);
        return spawnables.list[randomSpawnableIndex];
    }
}
