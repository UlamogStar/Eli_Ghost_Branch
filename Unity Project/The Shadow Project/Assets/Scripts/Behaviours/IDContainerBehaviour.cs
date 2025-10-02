/*
Originial Coder: Anthony R.
Recent Coder: Anthony R.
Recent Changes: Initial Coding
Last date worked on: 9/9/2025
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IDContainerBehaviour : MonoBehaviour
{
    public ID IDObj;
    public UnityEvent startEvent;

    public void Start()
    {
        startEvent.Invoke();
    }
}

