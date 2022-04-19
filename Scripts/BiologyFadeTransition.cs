using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiologyFadeTransition : MonoBehaviour
{
    [Header("GVR GameObjects")]
    //Gather all GVR Components inside the scene for transition
    public List<GameObject> GVR;

    public GameObject fadeObject, transitionCanvas;

    public Camera transCam, mainCam;

    public AudioSource IntroAudio;
    public GameObject IntroCanvas, Interactables;

    bool isFinished, faded = false;
    private void Awake()
    {
        Invoke("RemoveFadeAndCanvas", 1f);
    }

    private void RemoveFadeAndCanvas()
    {
        //Removes the transition scene and starts the intro audio
        fadeObject.SetActive(false);
        transitionCanvas.SetActive(false);
        faded = true;
    }

    private void FixedUpdate()
    {
        if (faded)
        {
            if (!isFinished)
            {
                //Scene Setup - Intro Audio and Intro Canvas
                isFinished = sceneSetup();
            }
        }
    }

    private bool sceneSetup()
    {
        if(transCam.fieldOfView >= 60)
        {
            //resets camera's FOV back to 60 (set to 120)
            transCam.fieldOfView -= (Time.deltaTime * 20f);
        }
        else
        {
            foreach (GameObject gvr in GVR)
            {
                //Grab all GVR in scene and activate it
                gvr.SetActive(true);
            }
            //Plays intro audio before the actual mode 1
            StartCoroutine(playAudio());
            transCam.gameObject.SetActive(false);
            mainCam.gameObject.SetActive(true);
            return true;
        }
        return false;
    }

    //Play Audio Coroutine
    IEnumerator playAudio()
    {
        IntroAudio.Play();
        IntroCanvas.SetActive(true);
        yield return new WaitForSeconds(IntroAudio.clip.length);
        yield return new WaitForSeconds(1f);
        IntroCanvas.SetActive(false);
        Interactables.SetActive(true);
    }
}