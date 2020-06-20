using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogueOnLoad : MonoBehaviour
{
    public Dialogue dialogue;
    public int startAt = 0;

    private DialogueHandler dh;
    // Start is called before the first frame update
    void Start()
    {
        dh = GameObject.FindGameObjectWithTag("DialogueBox").GetComponentInChildren<DialogueHandler>();

        if(dialogue != null)
        {
            dh.StartDialogue(dialogue, startAt);
        }      
    }
}
