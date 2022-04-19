using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteEventHandler : MonoBehaviour
{
    //Singleton Reference
    public static CompleteEventHandler current;

    private void Awake()
    {
        current = this;
    }


    /*
     * Acts as a listener for the interacted objects within the scene and
     * is independent from the variables, only the GameManager will access it
     */
    public event Action onCompleteStage;
    public void CompleteStageTrigger()
    {
        if(onCompleteStage != null)
        {
            onCompleteStage();
        }
    }
}