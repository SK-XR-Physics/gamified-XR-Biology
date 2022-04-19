using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiologyGameManager : MonoBehaviour
{
    //---- Locally Add points for now ----///
    public int currentPoints;

    //---- Stores all the interactable objects in the scene ----//
    public int interactableObjects;
    public int interactedObjects;

    public void AddPoints(int pointsToAdd)
    {
        currentPoints += pointsToAdd;
    }
}