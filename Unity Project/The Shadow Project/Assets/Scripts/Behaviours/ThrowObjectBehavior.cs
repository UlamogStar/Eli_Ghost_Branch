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
    public  Transform handLocation;

    public Dictionary<GameObject, Coroutine> currentThrows = new Dictionary<GameObject, Coroutine>();

    public void StartThrow(GameObject throwable, Vector3 location, float speed)
    {
        if (currentThrows.TryGetValue(throwable, out Coroutine existing))
        {
            StopCoroutine(existing);
            currentThrows.Remove(throwable);
        }
		ResetObjectBehaviour resetBehaviour = throwable.GetComponent<ResetObjectBehaviour>();
		resetBehaviour.StartTimeDestroy();

        Coroutine newThrow = StartCoroutine(MoveOverTime(throwable, location, speed));
        currentThrows[throwable] = newThrow;
        Debug.Log("Throw started");
    }

    public void Levitate(GameObject throwable, float speed)
    {
        
        
        Coroutine newThrow = StartCoroutine(MoveOverTimeLev(throwable, handLocation.position, speed));
        
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
        yield return new WaitForSeconds(0.75f);
        Debug.Log("Moving");
        Vector3 dir = (location - throwable.transform.position).normalized;
        Vector3 overShootTarget = location + dir * overshoot;

        while (throwable != null && Vector3.Distance(throwable.transform.position, overShootTarget) > 0.01f)
        {

            throwable.transform.position = Vector3.MoveTowards(
                throwable.transform.position,
                overShootTarget,
                speed * Time.deltaTime
            );

            yield return null;
        }

        // snap to end
        if (throwable != null){ throwable.transform.position = overShootTarget; }

        currentThrows.Remove(throwable);
    }
    
    public IEnumerator MoveOverTimeLev(GameObject throwable, Vector3 location, float speed)
    {
        yield return new WaitForSeconds(1);
        Vector3 dir = (location - throwable.transform.position).normalized;
        while (throwable != null && Vector3.Distance(throwable.transform.position, location) > 0.2f)
        {

            throwable.transform.position = Vector3.MoveTowards(
                throwable.transform.position,
                location,
                speed * Time.deltaTime
            );
            Debug.Log(Vector3.Distance(throwable.transform.position, location));
            yield return null;
        }

        // snap to end
        if (throwable != null){ throwable.transform.position = location; }

        currentThrows.Remove(throwable);
        Debug.Log("We're outta here");
    }
}
