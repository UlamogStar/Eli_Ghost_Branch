/*
Originial Coder: Zackery E.
Recent Coder: Zackery E.
Recent Changes: Now gives object behaviors a return to object.
Last date worked on: 9/29/2025
*/

using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public List<ObjectSpawner> enabledSpawns = new List<ObjectSpawner>();

    public List<ObjectBehaviour> spawnedObjects = new List<ObjectBehaviour>();

    public Transform returnToObject;

    public void spawnRandom(int amount)
    {
        for (int i = 0; i < amount; i++)
        {

            if (enabledSpawns.Count <= 0)
            {
                break;
            }

            // Get Random Spawner
            int randomSpawnerIndex = Random.Range(0, enabledSpawns.Count);
            ObjectSpawner randomSpawner = enabledSpawns[randomSpawnerIndex];

            // Spawn Random Object
            GameObject randomObject = randomSpawner.GetRandomObject();
            GameObject newObject = Instantiate(randomObject, randomSpawner.spawnLocation);

            // Add to list
            if (!newObject.GetComponent<ObjectBehaviour>())
            {
                Destroy(newObject);
                Debug.LogWarning("Object Prefab Missing: Object Behavior Componenet");
                return;
            }//end if
            ObjectBehaviour objectBehaviour = newObject.GetComponent<ObjectBehaviour>();
            objectBehaviour.originSpawn = randomSpawner;
            objectBehaviour.returnToObject = returnToObject;

            spawnedObjects.Add(objectBehaviour);

            // Disable Spawn (later re-enable)
            enabledSpawns.Remove(randomSpawner);

        }//end for
    }//end spawnRandom

    // Give it a spawned object that has been moved, and it will renable the spawn
    public void enableSpawnByObject(ObjectBehaviour objectBehavior)
    {
        if (!enabledSpawns.Contains(objectBehavior.originSpawn))
        {
            enabledSpawns.Add(objectBehavior.originSpawn);
        }//end if
    }//end enableSpawnByObject
}//end class
