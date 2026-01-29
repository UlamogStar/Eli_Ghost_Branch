/*
Originial Coder: Zackery E.
Recent Coder: Zackery E.
Recent Changes: Initial Coding
Last date worked on: 11/3/2025
*/

using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class HealthValueBehavior : MonoBehaviour
{
    public IntData health;
    public int weakness = 1;
    public UnityEvent onChange, onDamage, onDamDelay, onHeal;
    public float delayTime = 0.2f;

    public void Damage(int damage)
    {
        Debug.Log("Damage");
        health.value -= damage * weakness;
        onChange.Invoke();
        onDamage.Invoke();
        StartCoroutine(DelayEvent());
    }
    
    public void Heal(int heal)
    {
        health.value -= heal;
        onHeal.Invoke();
    }
    
    public IEnumerator DelayEvent()
    {
        yield return new WaitForSeconds(delayTime);
        onDamDelay.Invoke();
    }
}
