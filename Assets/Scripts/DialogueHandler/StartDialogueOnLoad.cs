using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogueOnLoad : MonoBehaviour
{
    public Dialogue dialogue;

    private DialogueHandler dh;
    // Start is called before the first frame update
    void Start()
    {
        dh = GameObject.FindGameObjectWithTag("DialogueBox").GetComponentInChildren<DialogueHandler>();

        if(dialogue != null)
        {
            dh.StartDialogue(dialogue);
        }      
    }
}
