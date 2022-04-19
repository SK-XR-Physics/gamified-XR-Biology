using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BiologyTransitionCamera : MonoBehaviour
{
    [Header("Objects")]
    public GameObject pointToLookAt;
    public GameObject player;
    public GameObject destination;
    public Transform fadeTransition;
    [Header("Camera")]
    public Camera transCam;
    [Header("Speed Values")]
    [SerializeField]
    private float rotSpeed;

    bool isFinishedRot = false;
    bool isFinishedZoom = false;

    private void FixedUpdate()
    {
        //Controls the Transition Camera's Movement
        if (!isFinishedRot)
            isFinishedRot = lookAt();
        else if(isFinishedRot)
            nextTransitionEvent();
    }

    private bool lookAt()
    {
        //Rotates the Transition Camera towards a specific point
        transCam.transform.rotation = Quaternion.Lerp(transCam.transform.rotation, Quaternion.FromToRotation(Vector3.forward, pointToLookAt.transform.position), Time.deltaTime * rotSpeed);
        if (transCam.transform.rotation == Quaternion.FromToRotation(Vector3.forward, pointToLookAt.transform.position))
        {
            return true;
        }
        return false;
    }

    /*
    IEnumerator lookAt()
    {
        yield return new WaitForSeconds(0.01f);
        transCam.transform.rotation = Quaternion.Lerp(transCam.transform.rotation, Quaternion.FromToRotation(Vector3.forward, pointToLookAt.transform.position), Time.deltaTime * rotSpeed);
        if(transCam.transform.rotation == Quaternion.FromToRotation(Vector3.forward, pointToLookAt.transform.position))
        {
            isFinishedRot = true;
        }
    }
    */

    private void nextTransitionEvent()
    {
        if (!isFinishedZoom)
            isFinishedZoom = zoomIn();
    }

    private bool zoomIn()
    {
        //-----The Actual Zoom Transition of the Camera-----//
        float zoomSpeed = 0.8f;
        if (transCam.fieldOfView < 120f)
        {
            //Slow Start of the transition camera
            transCam.fieldOfView += (Time.deltaTime * 20f);
        }
        else
        {
            //Then gradually speeds up
            zoomSpeed = 6f;
        }
        float step = zoomSpeed * Time.deltaTime;
        //Moving the transition Camera towards the point
        player.transform.position = Vector3.MoveTowards(player.transform.position, destination.transform.position, step);
        

        /// Final Check ///
        if(Vector3.Distance(player.transform.position, destination.transform.position) < 0.001f)
        {
            //Fade to black and switch scene
            fadeTransition.gameObject.SetActive(true);
            SceneManager.LoadScene("Mode1_Biology-InsideCell-AnimalCell-VR");
            return true;
        }
        return false;
    }
}