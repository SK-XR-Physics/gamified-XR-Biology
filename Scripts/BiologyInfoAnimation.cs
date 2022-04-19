using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BiologyInfoAnimation : MonoBehaviour
{
    [Header("Canvas Settings")]
    public GameObject infoCanvasUI;

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

    //Manually set how many interactables are in the Animation Data
    [SerializeField]
    private int _numOfInteractables;

    //The current audio data to be played
    private int _currentAudioToPlay;

    public BiologyGameManager gameManager;

    //Animation Data for the object to play
    [SerializeField]
    private Animator _animData;

    [Header("Info Container")]
    public List<GlobalInteractableBiology> infoContainer;

    private bool isResetting;

    private void Awake()
    {
        //currentUser.GameObject.Find("CurrentUser").GetComponent<CurrentUser>();
    }

    private void Start()
    {
        gameManager.interactableObjects = _numOfInteractables;
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
                        if (!isResetting)
                            playAnim();
                        else if (isResetting)
                        {
                            resetAnim();
                            isResetting = false;
                        } 
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
    public void gazeAtObject() //Pointer Enter for moving forward
    {
        isResetting = false;
        if (toggleTouchGaze.isGaze)
        {
            isRadialFilled = false;
            isObjectGazed = true;
        }
    }
    public void gazeAtObjectResetting() //Pointer Enter for resetting
    {
        isResetting = true;
        if (toggleTouchGaze.isGaze)
        {
            isRadialFilled = false;
            isObjectGazed = true;
        }
    }

    #region SHOW_INFO
    public void playAnim() //Pointer Click (if not gaze)
    {
        if (!isInteracting)
            StartCoroutine(animCycle(infoContainer[_currentAudioToPlay].AudioDesc.length));
    }

    IEnumerator animCycle(float timeToWait)
    {
        isInteracting = true;
        fillInfo();
        infoCanvasUI.SetActive(true);

        //Pass to Audio Manager
        audioManager.getAudioInfoToPlay(infoContainer[_currentAudioToPlay].AudioDesc);

        //Play the Animation
        _animData.SetTrigger("nextAnim");
        

        //Checks if the user already interacted with it to score points
        if (!isAlreadyInteracted)
        {
            //isAlreadyInteracted = true;
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

        

        if (_currentAudioToPlay < gameManager.interactableObjects)
            _currentAudioToPlay++;
    }
    #endregion

    
    void fillInfo() //Fill all necessary informations retrieving from the SO
    {
        Name.text = infoContainer[_currentAudioToPlay].Name;
        Desc.text = infoContainer[_currentAudioToPlay].Description;
        Disp.sprite = infoContainer[_currentAudioToPlay].ImageDisplay;
    }
    
    void fillInfoReset()
    {
        Name.text = infoContainer[_currentAudioToPlay - 1].Name;
        Desc.text = infoContainer[_currentAudioToPlay - 1].Description;
        Disp.sprite = infoContainer[_currentAudioToPlay - 1].ImageDisplay;
    }

    public void resetGaze() //Pointer Exit
    {
        isObjectGazed = false;
        gazeTimer = 0f;
        radialImage.fillAmount = 0f;
    }


    public void resetAnim()
    {
        isResetting = false;
        switch (_currentAudioToPlay)
        {
            case 1:
                {
                    _animData.SetTrigger("replayStage1");
                    break;
                }
            case 2:
                {
                    _animData.SetTrigger("replayStage2");
                    break;
                }
            case 3:
                {
                    _animData.SetTrigger("replayStage3");
                    break;
                }
            case 4:
                {
                    _animData.SetTrigger("replayStage4");
                    break;
                }
            case 5:
                {
                    _animData.SetTrigger("replayStage5");
                    break;
                }
            case 6:
                {
                    _animData.SetTrigger("replayStage6");
                    break;
                }
        }
        audioManager.getAudioInfoToPlay(infoContainer[(_currentAudioToPlay - 1)].AudioDesc);
        if (!isInteracting)
            StartCoroutine(animCycleReset(infoContainer[_currentAudioToPlay - 1].AudioDesc.length));
    }

    IEnumerator animCycleReset(float timeToWait)
    {
        isInteracting = true;
        fillInfoReset();
        yield return new WaitForSeconds(timeToWait);
        isInteracting = false;
    }
}