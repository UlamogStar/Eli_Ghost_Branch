/*
Originial Coder: Zackery E.
Recent Coder: Zackery E.
Recent Changes: Initial Coding
Last date worked on: 9/25/2025
*/

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewObjectList", menuName = "Data Object/Lists/ObjectList")]
public class ObjectListSO : ScriptableObject
{
    public List<GameObject> list = new List<GameObject>();
}
