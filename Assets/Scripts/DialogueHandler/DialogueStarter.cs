using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStarter : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject button;
    private bool playerEnteredTrigger = false;
    private bool dialogueStarted = false;
    private DialogueHandler dialogueHandler;

    private void Start()
    {
        if(dialogue == null)
        {
            dialogue = Resources.Load("Dialogues/EmptyDialogue") as Dialogue;
        }

        dialogueHandler = GameObject.FindGameObjectWithTag("DialogueBox").GetComponentInChildren<DialogueHandler>();
        if(dialogueHandler == null)
        {
            Debug.LogError("No DialogueHander found in current scene. There may occur further errors");
        }
        else
        {
            dialogueHandler.dialogueEndedEventHandler += SetDialogueEndedBool;
        }
        if (button == null)
        {
            button = Instantiate(Resources.Load("Buttons/E") as GameObject, new Vector3(transform.position.x, transform.position.y), new Quaternion(0, 0, 0, 0), this.gameObject.transform);
            button.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            playerEnteredTrigger = true;
            button.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            playerEnteredTrigger = false;
            button.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private void Update()
    {
        if(playerEnteredTrigger && !dialogueStarted && Input.GetButtonDown("Interact"))
        {
            dialogueStarted = true;
            dialogueHandler.StartDialogue(dialogue);
        }
    }

    public void SetDialogueEndedBool(object sender, EventArgs e)
    {
        dialogueStarted = false;
    }
}
