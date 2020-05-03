using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueHandler : MonoBehaviour
{
    [Header("UI Elements")]
    public Text dialogueText;
    public Image speakerLeft;
    public Image speakerRight;
    [Header("----------")]
    public Dialogue dialogue;
    public string language = "english";
    public event EventHandler dialogueEndedEventHandler;

    private bool dialogeFinishedPrinting;

    public void StartDialogue(Dialogue dialogue)
    {
        this.dialogue = dialogue;
        if (dialogueText != null && speakerLeft != null && speakerRight != null)
        {
            StartCoroutine("PrintDialogue");
        }
        else
        {
            Debug.LogError("UI elements not set correctly.");
        }
    }

    IEnumerator PrintDialogue()
    {
        bool dialogueHasEnded = false;
        int i = 0;
        dialogueText.text = "";

        dialogueText.enabled = true;

        while (!dialogueHasEnded)
        {
            DialogueElement currElement = dialogue.dialogueItems[i];
            string currentText = GetDialogueTextInCurrentLanguage(currElement);

            if (currElement.speakerPosition == DialogueElement.AvatarPos.left)
            {
                speakerLeft.enabled = true;
                speakerLeft.sprite = currElement.speakerPic;
            }
            else if (currElement.speakerPosition == DialogueElement.AvatarPos.right)
            {
                speakerRight.enabled = true;
                speakerRight.sprite = currElement.speakerPic;
            }
            StartCoroutine("PrintDialogueItem", currElement);

            yield return new WaitForSeconds(0.05f);
            while (!Input.GetButtonDown("Interact"))
            {
                yield return null;
            }

            //Print whole Dialogue Element after Interaced is pressed a second time
            if (!dialogeFinishedPrinting)
            {
                StopCoroutine("PrintDialogueItem");
                dialogueText.text = currentText;
                yield return new WaitForSeconds(0.05f);
                while (!Input.GetButtonDown("Interact"))
                {
                    yield return null;
                }
            }
            //ClenUp of old DialogueItem
            StopCoroutine("PrintDialogueItem");
            dialogueText.text = "";

            i++;
            if(i >= dialogue.dialogueItems.Count)
            {
                dialogueHasEnded = true;
            }
        }
        dialogueEndedEventHandler.Invoke(this,null);
        ChangeEnabledStateOfUiObjects(false);
    }

    IEnumerator PrintDialogueItem(DialogueElement elem)
    {
        string text = GetDialogueTextInCurrentLanguage(elem);
        dialogeFinishedPrinting = false;
        int j = 0;
        while(j < text.Length)
        {
            dialogueText.text += text[j];
            j++;
            yield return new WaitForSeconds(elem.playbackSpeed);
        }
        dialogeFinishedPrinting = true;
    }

    private void ChangeEnabledStateOfUiObjects(bool state)
    {
        speakerLeft.enabled = state;
        speakerRight.enabled = state;
        dialogueText.enabled = state;
    }

    private string GetDialogueTextInCurrentLanguage(DialogueElement elem)
    {
        if(language == "german")
        {
            return elem.dialogueTextGerman;
        }
        else
        {
            return elem.dialogueTextEnglish;
        }
    }
}
