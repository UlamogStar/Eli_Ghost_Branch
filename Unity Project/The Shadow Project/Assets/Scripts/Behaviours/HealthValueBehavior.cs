/*
Originial Coder: Zackery E.
Recent Coder: Zackery E.
Recent Changes: Initial Coding
Last date worked on: 11/3/2025
*/

using UnityEngine;
using UnityEngine.Events;

public class HealthValueBehavior : MonoBehaviour
{
    public IntData health;
    public int weakness = 1;
    public UnityEvent onChange, onDamage, onHeal;

    public void Damage(int damage)
    {
        Debug.Log("Damage");
        health.value -= damage * weakness;
        onChange.Invoke();
        onDamage.Invoke();
    }
    
    public void Heal(int heal)
    {
        health.value -= heal;
        onHeal.Invoke();
    }
    
    
}
