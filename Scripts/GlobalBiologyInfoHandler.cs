using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalBiologyInfoHandler : MonoBehaviour
{
    [Header("Canvas Settings")]
    public GameObject infoCanvasUI;

    [Header("SO Parts")]
    [SerializeField]
    private GlobalInteractableBiology infoContainer;

    [Header("Audio Manager")]
    [SerializeField]
    private BiologyAudioManager audioManager;

    [Header("Gaze")]
    public ToggleTouchGaze toggleTouchGaze;

    public float gazeTimer;
    public Image radialImage;
    public bool isRadialFilled;
    public bool isObjectGazed;

    [Header("UI/Audio Settings")]
    public Text Name;
    public Text Desc;
    public Image Disp;
    public GameObject pointsUI;
    private bool isAlreadyInteracted = false;

    //A temporary workaround so that it cannot double click an interactable
    private bool isInteracting;

    [SerializeField]
    private GameObject[] interactables;

    public BiologyGameManager gameManager;

    private void Awake()
    {
        //currentUser.GameObject.Find("CurrentUser").GetComponent<CurrentUser>();
    }

    private void Start()
    {
        //Get all interactables in the scene
        interactables = GameObject.FindGameObjectsWithTag("Interactable");
        gameManager.interactableObjects = interactables.Length;
    }

    private void LateUpdate()
    {
        //Checks if the object is being looked at
        if (isObjectGazed)
        {
            if (!isRadialFilled)
            {
                //Fill Radial loading icon (Green Circle)
                gazeTimer += Time.deltaTime;
                radialImage.fillAmount = gazeTimer / 2;

                if (gazeTimer >= 2)
                {
                    if (gameManager.interactedObjects <= gameManager.interactableObjects)
                    {
                        isRadialFilled = true;
                        resetInfo();
                        resetGaze();
                        showInfo();
                    }
                    else
                    {
                        resetGaze();
                        Invoke("complete", 1f);
                    }
                }
            }
        }
    }

    //Reset Info Function to avoid information jumbling
    private void resetInfo()
    {
        Name.text = "";
        Desc.text = "";
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

    #region SHOW_INFO
    public void showInfo() //Pointer Click (if not gaze)
    {
        if (!isInteracting)
            StartCoroutine(infoCycle(infoContainer.AudioDesc.length));
    }

    IEnumerator infoCycle(float timeToWait)
    {
        isInteracting = true;
        fillInfo();
        infoCanvasUI.SetActive(true);

        //Pass to Audio Manager
        audioManager.getAudioInfoToPlay(infoContainer.AudioDesc);
        //Checks if the user already interacted with it to score points
        if (!isAlreadyInteracted)
        {
            isAlreadyInteracted = true;
            gameManager.AddPoints(50);
            pointsUI.SetActive(true);
            gameManager.interactedObjects++;
        }

        if (gameManager.interactedObjects == gameManager.interactableObjects)
        {
            CompleteEventHandler.current.CompleteStageTrigger();
        }
        yield return new WaitForSeconds(timeToWait);
        isInteracting = false;
    }
    #endregion

    void fillInfo() //Fill all necessary informations retrieving from the SO
    {
        Name.text = infoContainer.Name;
        Desc.text = infoContainer.Description;
        Disp.sprite = infoContainer.ImageDisplay;
    }

    public void resetGaze() //Pointer Exit
    {
        isObjectGazed = false;
        gazeTimer = 0f;
        radialImage.fillAmount = 0f;
    }
}