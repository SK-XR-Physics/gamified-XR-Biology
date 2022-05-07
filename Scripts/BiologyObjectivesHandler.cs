using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BiologyObjectivesHandler : MonoBehaviour
{
    [Header("Audio Manager")]
    public BiologyAudioManager audioManager;

    [Header("Objectives Text")]
    public TextMeshProUGUI objText;
    public TextMeshProUGUI objText1;
    public TextMeshProUGUI objText2;

    [Header("Audios to Play")]
    public List<AudioClip> audios;

    [Header("Image Displayer")]
    public GameObject imageGO;
    public Image actualImage;

    [Header("Game Manager")]
    public BiologyGameManager gameManager;

    [SerializeField]
    private int _allInteractableObjects;

    [Header("Points UI")]
    public GameObject pointsUI;

    private void Start()
    {
        gameManager.interactableObjects = _allInteractableObjects;
        objText.text = "Inspect all Parts.";
        objText1.text = "Find the Lysosome.";
        GameEventListeners.current.onGoalInteracted += UpdateObjectives_GoalFound;
        GameEventListeners.current.onAllInteracted += UpdateObjectives_AllFound;
        GameEventListeners.current.onNextPhase += UpdateObjectives_NextPhase;
    }


    GlobalInteractableGame currentRetrievedInfo;
    //Will play Audio based on the index number provided in the parameter
    bool thisAlreadyInteracted;
    public bool isInteracted(bool isInteracted)
    {
        if (!isInteracted)
        {
            return true;
        }
        else if (isInteracted)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void InteractiveUpdate(int index, Sprite rawImage, bool isAlreadyInteracted)
    {
        //!isAlreadyInteracted
        if (isAlreadyInteracted == false)
        {
            gameManager.interactedObjects++;
            pointsUI.SetActive(true);
        }

        if(gameManager.interactedObjects == gameManager.interactableObjects)
        {
            GameEventListeners.current.AllInteracted();
        }

        if(index == 2)
        {
            //Lysosome Found!
            GameEventListeners.current.GoalInteracted();
        }

        audioManager.getAudioInfoToPlay(audios[index]);
        StartCoroutine(displayImage(rawImage));
    }

    public void GetInfo(GlobalInteractableGame infoContainers, bool isAlreadyInteracted)
    {
        currentRetrievedInfo = infoContainers;
        thisAlreadyInteracted = isAlreadyInteracted;
    }

    //Click to Inspect Object and Play the Audio
    bool alreadyOnNextPhase = false;
    public void OnClick_ButtonInspect()
    {
        InteractiveUpdate(currentRetrievedInfo.indexIdentity, currentRetrievedInfo.image, thisAlreadyInteracted);
        if(!alreadyOnNextPhase)
            if (gameManager.interactedObjects == gameManager.interactableObjects)
            {
                GameEventListeners.current.AllInteracted();
                alreadyOnNextPhase = true;
            }
                
    }

    public void OnClick_ButtonInject()
    {
        //Inject Action
        if(!gameManager.isNextPhase)
        {
            pointsUI.SetActive(true);
            GameEventListeners.current.nextPhaseAction();
        }  
        else
        {
            GameEventListeners.current.Injected();
        }
    }

    private void LateUpdate()
    {
        if(gameManager.isNextPhase)
            objText1.text = "" + gameManager.currentEnemiesKilled + " / " + gameManager.allEnemies;
    }

    IEnumerator displayImage(Sprite rawImage)
    {
        imageGO.SetActive(true);
        actualImage.sprite = rawImage;
        yield return new WaitForSeconds(5.5f);
        imageGO.SetActive(false);
    }

    private void UpdateObjectives_GoalFound()
    {
        //Listens for a Trigger when the main Goal is Found
        //Lists as the object is found
        gameManager.isLysosomeInspected = true;

        //Debug.Log("UpdateObjectives_GoalFound() Fired");
        pointsUI.SetActive(true);
        objText1.fontStyle = FontStyles.Strikethrough;
        objText2.text = "Inject RNA to the Lysosome";

        //5th Element is the audio for when you have found the Lysosome
        audioManager.getAudioInfoToPlay(audios[5]);
    }

    private void UpdateObjectives_AllFound()
    {
        if (gameManager.isLysosomeInspected)
        {
            audioManager.getAudioInfoToPlay(audios[7]);
        }
        else if (!gameManager.isLysosomeInspected)
        {
            audioManager.getAudioInfoToPlay(audios[6]);
        }
        pointsUI.SetActive(true);
        objText.fontStyle = FontStyles.Strikethrough;
    }

    private void UpdateObjectives_NextPhase()
    {
        //Listens for a Trigger when moving on the Next Phase
        audioManager.getAudioInfoToPlay(audios[8]);
        objText1.fontStyle = FontStyles.Normal;
        objText2.text = "";
        objText.text = "Kill Viruses ";
    }

    private void OnApplicationQuit()
    {
        GameEventListeners.current.onGoalInteracted -= UpdateObjectives_GoalFound;
        GameEventListeners.current.onAllInteracted -= UpdateObjectives_AllFound;
        GameEventListeners.current.onNextPhase -= UpdateObjectives_NextPhase;
    }
}