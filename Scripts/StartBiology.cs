using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBiology : MonoBehaviour
{
    public GameObject introPanel;

    //public Animator anim;
    void Start()
    {
        Invoke("StartInteraction", 17f);
    }

    void StartInteraction()
    {
        //introPanel.SetActive(false);

        //Moves to transition scene
        SceneManager.LoadScene("Mode1_Biology-CellBiologyTransition-VR");
    }
}