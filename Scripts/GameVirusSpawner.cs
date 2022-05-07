using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVirusSpawner : MonoBehaviour
{
    public int numberOfEnemies;
    private BiologyGameManager gameManager;
    public GameObject virusGO1, virusGO2;
    void Start()
    {
        gameManager = FindObjectOfType<BiologyGameManager>();

        gameManager.allEnemies = numberOfEnemies;
        SpawnViruses();
    }

    void SpawnViruses()
    {
        for(int i = 0; i < numberOfEnemies; i++)
        {
            float horizontal = Random.Range(-17, 18);
            float vertical = Random.Range(-17, 18);
            Vector3 spawnPos = new Vector3(horizontal, 1, vertical);

            int num = Random.Range(1, 3);
            switch (num)
            {
                case 1:
                    {
                        Instantiate(virusGO1, spawnPos, Quaternion.identity);
                        continue;
                    }
                case 2:
                    {
                        Instantiate(virusGO2, spawnPos, Quaternion.identity);
                        continue;
                    }
            }
        }
    }
}