using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogue : Interactable
{
    [SerializeField]
    private GameObject dialogue;
    private bool isActive = true;

    public override void Interact()
    {
        dialogue.SetActive(isActive);
    }
}
