/*
Originial Coder: Zackery E.
Recent Coder: Zackery E.
Recent Changes: Initial Coding
Last date worked on: 9/25/2025
*/

using UnityEngine;
using UnityEngine.Events;

public class MonoEventsBehavior : MonoBehaviour
{
    public UnityEvent startEvent, awakeEvent, disableEvent;

    private void Awake()
    {
        awakeEvent.Invoke();
    }

    private void Start()
    {
        startEvent.Invoke();
    }

    private void OnDisable()
    {
        disableEvent.Invoke();
    }
}
