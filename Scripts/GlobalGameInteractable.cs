using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalGameInteractable : MonoBehaviour
{
    [SerializeField]
    private GlobalInteractableGame infoContainer;

    public Button inspectButton, injectButton;

    [SerializeField]
    private int _allInteractables;

    [SerializeField]
    private bool thisAlreadyInteracted = false;

    [Header("Objectives Handler")]
    public BiologyObjectivesHandler objHandler;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(infoContainer.name == "Lysosome")
            {
                //Also Enable Inject
                injectButton.interactable = true;
            }
            //Enable Inspect Button
            inspectButton.interactable = true;
            objHandler.GetInfo(infoContainer, thisAlreadyInteracted);
            thisAlreadyInteracted = objHandler.isInteracted(thisAlreadyInteracted);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (infoContainer.name == "Lysosome")
            {
                //Disable Inject Button
                injectButton.interactable = false;
            }
            //Disable Inspect Button
            inspectButton.interactable = false;
        }
    }
}