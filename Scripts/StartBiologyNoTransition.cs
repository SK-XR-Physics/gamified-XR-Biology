using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBiologyNoTransition : MonoBehaviour
{
    public AudioSource audioStart;
    public GameObject introCanvas;
    public GameObject interactablesCanvas;

    private void Start()
    {
        audioStart.Play();
        Invoke("playSubject", audioStart.clip.length);
    }

    //------ Start of Subject ------//
    void playSubject()
    {
        introCanvas.SetActive(false);
        interactablesCanvas.SetActive(true);
    }
}