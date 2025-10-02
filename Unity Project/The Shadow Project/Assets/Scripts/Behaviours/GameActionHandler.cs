/*
Originial Coder: Anthony R.
Recent Coder: Anthony R.
Recent Changes: Initial Coding
Last date worked on: 9/9/2025
*/
using UnityEngine;
using UnityEngine.Events;

public class GameActionHandler : MonoBehaviour
{
    public GameAction gameActionObj;
    public UnityEvent onRaiseEvent;

    private void Start()
    {
        gameActionObj.raise += Raise;
    }

    private void Raise()
    {
        onRaiseEvent.Invoke();
    }
}