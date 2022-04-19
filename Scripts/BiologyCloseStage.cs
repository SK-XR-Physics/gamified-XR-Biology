using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BiologyCloseStage : MonoBehaviour
{
    [Header("Gaze Controller")]
    public ToggleTouchGaze toggleTouchGaze;

    public float gazeTimer;
    public Image radialImage;
    public bool isRadialFilled;
    public bool isObjectGazed;

    private void Update()
    {
        if (isObjectGazed)
        {
            if (!isRadialFilled)
            {
                gazeTimer += Time.deltaTime;
                radialImage.fillAmount = gazeTimer / 2;

                if (gazeTimer >= 2)
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
        SceneManager.LoadScene("Dashboard");
    }
}