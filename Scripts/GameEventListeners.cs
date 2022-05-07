using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventListeners : MonoBehaviour
{
    public static GameEventListeners current;

    //Singleton Reference
    private void Awake()
    {
        current = this;
    }


    public event Action onAllInteracted;
    public void AllInteracted()
    {
        if (onAllInteracted != null)
        {
            onAllInteracted();
        }
    }

    public event Action onGoalInteracted;
    public void GoalInteracted()
    {
        if(onGoalInteracted != null)
        {
            onGoalInteracted();
        }
    }

    public event Action onNextPhase;
    public void nextPhaseAction()
    {
        if(onNextPhase != null)
        {
            onNextPhase();
        }
    }

    public event Action onInjected;
    public void Injected()
    {
        if(onInjected != null)
        {
            onInjected();
        }
    }

    public event Action onModuleFinished;
    public void Completed()
    {
        if(onModuleFinished != null)
        {
            onModuleFinished();
        }
    }
}