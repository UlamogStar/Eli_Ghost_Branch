/*
Originial Coder: Zackery E.
Recent Coder: Zackery E.
Recent Changes: Initial Coding
Last date worked on: 9/29/2025
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObjectBehavior : MonoBehaviour
{

    [Header("Settings")]
    public float overshoot = 3; // How far the object goes past target

    public Dictionary<GameObject, Coroutine> currentThrows = new Dictionary<GameObject, Coroutine>();

    public void StartThrow(GameObject throwable, Vector3 location, float speed)
    {
        if (currentThrows.TryGetValue(throwable, out Coroutine existing))
        {
            StopCoroutine(existing);
            currentThrows.Remove(throwable);
        }

        Coroutine newThrow = StartCoroutine(MoveOverTime(throwable, location, speed));
        currentThrows[throwable] = newThrow;
    }

    public void EndThrow(GameObject throwable)
    {
        if (currentThrows.TryGetValue(throwable, out Coroutine throwRoutine))
        {
            StopCoroutine(throwRoutine);
            currentThrows.Remove(throwable);   
        }
    }

    public IEnumerator MoveOverTime(GameObject throwable, Vector3 location, float speed)
    {
        Vector3 dir = (location - throwable.transform.position).normalized;
        Vector3 overShootTarget = location + dir * overshoot;

        while (Vector3.Distance(throwable.transform.position, location) > 0.01f)
        {

            throwable.transform.position = Vector3.MoveTowards(
                throwable.transform.position,
                overShootTarget,
                speed * Time.deltaTime
            );

            yield return null;
        }

        // snap to end
        throwable.transform.position = overShootTarget;

        currentThrows.Remove(throwable);
    }
}
