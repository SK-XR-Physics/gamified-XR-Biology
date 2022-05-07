using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiBodyInteract : MonoBehaviour
{
    private BiologyGameManager gameManager;
    private void Start()
    {
        gameManager = FindObjectOfType<BiologyGameManager>();
        Invoke("DeathByTime", 15);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(gameManager.heldAntibodies >= 3)
            {
                //Do Nothing
            }
            else
            {
                gameManager.heldAntibodies++;
                Destroy(this.gameObject);
            }
        }
    }

    void DeathByTime()
    {
        Destroy(this.gameObject);
    }
}