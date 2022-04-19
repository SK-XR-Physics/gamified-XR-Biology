using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBiologyNoTransWithAnim : MonoBehaviour
{
    public AudioSource audioStart;
    public GameObject introCanvas;
    public GameObject infoCanvasUI;

    private void Start()
    {
        audioStart.Play();
        Invoke("playSubject", audioStart.clip.length);
    }

    //------ Start of Subject ------//
    void playSubject()
    {
        introCanvas.SetActive(false);
        infoCanvasUI.SetActive(true);
    }
}