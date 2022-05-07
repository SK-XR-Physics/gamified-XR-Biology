using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biology2DCameraLogic : MonoBehaviour
{
    public GameObject messageOverlay;
    public GameObject controlsUI;

    bool orientedCorrect = false;
    void LateUpdate()
    {
        if (!orientedCorrect)
        {
            if(Input.deviceOrientation == DeviceOrientation.LandscapeLeft || 
               Input.deviceOrientation == DeviceOrientation.LandscapeRight)
            {
                orientedCorrect = true;
                messageOverlay.SetActive(false);
                controlsUI.SetActive(true);
                //Screen Oriented Correctly
            }
        }
        else if (orientedCorrect)
        {
            if(Input.deviceOrientation == DeviceOrientation.Portrait)
            {
                orientedCorrect = false;
                messageOverlay.SetActive(true);
                controlsUI.SetActive(false);
                //Rotate Screen to Portrait
                //But must rotate to Landscape
            }
        }
    }
}