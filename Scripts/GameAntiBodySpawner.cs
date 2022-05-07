using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAntiBodySpawner : MonoBehaviour
{
    public GameObject antibodyGO;
    void Start()
    {
        Spawn();
    }

    //Keeps spawning every x second/s
    void Spawn()
    {
        var instance = Instantiate(antibodyGO, transform.position, Quaternion.identity);
        Invoke("Spawn", 6);
    }
}