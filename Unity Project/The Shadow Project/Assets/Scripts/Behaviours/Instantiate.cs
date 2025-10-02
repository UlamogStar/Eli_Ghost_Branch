/*
Originial Coder: Zackery E.
Recent Coder: Owynn A.
Recent Changes: Formatting and Commenting
Last date worked on: 9/16/2025
*/

using System.Collections.Generic;
using UnityEngine;

public class Instantiate : MonoBehaviour
{
    public List<GameObject> spawnLocations; // Locations they can be spawned
    public List<GameObject> spawnables; // Objects you can spawn

    public GameObject spawnItemsParent; // Can be null

    public void SpawnItemsRandomly(int amount) // Spawn Items in random Locations
    {
        List<GameObject> remainingSpawns = new List<GameObject>(spawnLocations);
        List<GameObject> remainingObjects = new List<GameObject>(spawnables);

        for (int i = 0; i < amount; i++)
        {
            if (remainingObjects.Count <= 0)
            {
                remainingObjects = new List<GameObject>(spawnables); // If no more objects, restock items
            }

            if (remainingSpawns.Count <= 0)
            {
                break; // If out of spawns, don't spawn anymore
            }

            // Get Random Location
            int randomNum2 = Random.Range(0, remainingSpawns.Count);
            GameObject selectedSpawn = remainingSpawns[randomNum2];

            // Get Random Item
            int randomNum = Random.Range(0, remainingObjects.Count);
            GameObject selectedItem = remainingObjects[randomNum];

            Debug.Log("Item spawned");
            GameObject newObject = Instantiate(selectedItem, selectedSpawn.transform);

            remainingObjects.Remove(selectedItem);
            remainingSpawns.Remove(selectedSpawn);

            // If parent object, set as child
            if (spawnItemsParent != null)
            {
                newObject.transform.SetParent(spawnItemsParent.transform);
            }//end if
        }//end for
    }//end spawnItemsRandomly

    public void SpawnItemsInOrder()
    {
        for (int i = 0; i < spawnables.Count; i++)
        {
            if (i >= spawnLocations.Count) // if out of locations, stop spawning
			{
				break;
			} 

            Instantiate(spawnables[i], spawnLocations[i].transform);

        }//end for
    }//end spawnInOrder
}//end class
