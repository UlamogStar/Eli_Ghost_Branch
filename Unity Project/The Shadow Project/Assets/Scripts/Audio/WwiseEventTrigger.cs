using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WwiseEventTrigger : MonoBehaviour
{
    //which event should trigger?
    public AK.Wwise.Event WwiseEvent1;
    public AK.Wwise.Event WwiseEvent2;
    public AK.Wwise.Event WwiseEvent3;

    //where should the sound come from?
    public GameObject SoundSource;

    public void EventTrigger1()
    {
        WwiseEvent1.Post(SoundSource);
    }

    public void EventTrigger2()
    {
        WwiseEvent2.Post(SoundSource);
    }

    public void EventTrigger3()
    {
        WwiseEvent3.Post(SoundSource);
    }
}

