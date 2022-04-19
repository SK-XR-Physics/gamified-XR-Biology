using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BiologyCloseInfoUI : MonoBehaviour
{
    public GameObject infoCanvasUI;
    public GameObject infoGO;
    public GameObject completeGO;

    [Header("Gaze Controller")]
    public ToggleTouchGaze toggleTouchGaze;

    public float gazeTimer;
    public Image radialImage;
    public bool isRadialFilled;
    public bool isObjectGazed;

    private bool isAlreadyComplete = false;

    private void Start()
    {
        //Subscribing to the event handler to listen to the events being triggered
        CompleteEventHandler.current.onCompleteStage += completeStage;
    }

    private void Update()
    {
        if (isObjectGazed)
        {
            if (!isRadialFilled)
            {
                gazeTimer += Time.deltaTime;
                radialImage.fillAmount = gazeTimer / 2;

                if(gazeTimer >= 2)
                {
                    resetProgress();
                    isRadialFilled = true;
                    closeThis();
                }
            }
        }
    }


    //-------Buttons for Event Systems-------//
    public void gazeAtObject() //Pointer Enter
    {
        if (toggleTouchGaze.isGaze)
        {
            isRadialFilled = false;
            isObjectGazed = true;
        }
    }

    public void resetProgress() //Pointer Exit
    {
        isObjectGazed = false;
        gazeTimer = 0f;
        radialImage.fillAmount = 0f;
    }

    public void closeThis() //Pointer Click
    {
        if(!isAlreadyComplete)
            infoCanvasUI.SetActive(false);
        else
        {
            infoGO.SetActive(false);
            completeGO.SetActive(true);
        }
    }

    //Tells the 'close button' to end the current stage and added an extra parameter to activate it
    private void completeStage()
    {
        isAlreadyComplete = true;
    }

    //Unsubscribes to the current event handler to reduce garbage in code
    private void OnApplicationQuit()
    {
        CompleteEventHandler.current.onCompleteStage -= completeStage;
    }
}