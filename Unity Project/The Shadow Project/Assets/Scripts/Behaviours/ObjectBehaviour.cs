/*
Originial Coder: Owynn A.
Recent Coder: Zackery E.
Recent Changes: Added Damaging Behvaior
Last date worked on: 11/3/2025
*/

using UnityEngine;

public class ObjectBehaviour : MonoBehaviour
{
    [Header("Settings")]
    public ObjectSize size = ObjectSize.Small;
    public int damage = 10;
    public float throwSpeed = 1f;
    public bool returnable = false, thrown = false, returned = false, hit = false;

    [Header("Info")]
    public ObjectSpawner originSpawn;

    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out HealthValueBehavior healthValue) && hit == false)
        {
            if (other.CompareTag("Ghost") && returned == true)
            {
                healthValue.Damage(damage);
                hit = true;
            } else if (other.CompareTag("Player"))
            {
                healthValue.Damage(damage);
                hit = true;
            }
        }
    }
}

// Object sizes must be from small -> large
public enum ObjectSize
{
    Small, // 0
    Medium,    
    Big // 1
}