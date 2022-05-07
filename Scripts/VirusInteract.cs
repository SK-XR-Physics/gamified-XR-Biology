using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirusInteract : MonoBehaviour
{
    private BiologyGameManager gameManager;
    private GlobalGameInteractable interactables;
    private Button injectBtn;

    //GameObject of Points Popup
    private GameObject pointsUI;

    private BiologyAudioManager audioManager;
    [SerializeField]
    private AudioClip clipToPlay;

    private bool isCurrentlyInRange = false;
    void Start()
    {
        gameManager = FindObjectOfType<BiologyGameManager>();
        interactables = FindObjectOfType<GlobalGameInteractable>();
        injectBtn = interactables.injectButton;
        pointsUI = FindObjectOfType<UIPoints>().gameObject;
        audioManager = FindObjectOfType<BiologyAudioManager>();

        GameEventListeners.current.onInjected += Die;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            injectBtn.interactable = true;
            isCurrentlyInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            injectBtn.interactable = false;
            isCurrentlyInRange = false;
        }
    }

    private void Die()
    {
        if(gameManager.heldAntibodies > 0)
        {
            if (isCurrentlyInRange)
            {
                gameManager.currentEnemiesKilled++;
                gameManager.heldAntibodies--;
                pointsUI.SetActive(true);
                GameEventListeners.current.onInjected -= Die;
                if (gameManager.currentEnemiesKilled == gameManager.allEnemies)
                {
                    GameEventListeners.current.Completed();
                }
                Destroy(this.gameObject);
            }
            if (gameManager.heldAntibodies == 0)
                audioManager.getAudioInfoToPlay(clipToPlay);
        }
        else
        {
            audioManager.getAudioInfoToPlay(clipToPlay);
        }
    }




    private void OnApplicationQuit()
    {
        GameEventListeners.current.onInjected -= Die;
    }
}