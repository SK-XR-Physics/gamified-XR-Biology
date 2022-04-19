using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Interactable", menuName = "Biology Interactables/New Interactable")]
public class GlobalInteractableBiology : ScriptableObject
{
    //----- Biology Global Interactables Information Container -----//
    public string Name;
    public string Description;

    public Sprite ImageDisplay;
    public AudioClip AudioDesc;
}