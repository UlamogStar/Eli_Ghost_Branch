/*
Originial Coder: Anthony R.
Recent Coder: Owynn A.
Recent Changes: Changed asset menu loaction
Last date worked on: 9/9/2025
*/
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ID", menuName = "Scriptable Objects/GameAction")]
public class GameAction : ScriptableObject
{
    public UnityAction raise;

    public void Raise()
    {
        raise?.Invoke();
    }
}