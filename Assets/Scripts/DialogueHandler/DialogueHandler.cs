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

    private bool dialogeFinishedPrinting;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("PrintDialogue");
    }

    // Update is called once per frame
    void Update()
    {
        
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

            yield return new WaitForSeconds(0.5f);
            while (!Input.GetButtonDown("Interact"))
            {
                yield return null;
            }

            //Print whole Dialogue Element after Interaced is pressed a second time
            if (!dialogeFinishedPrinting)
            {
                StopCoroutine("PrintDialogueItem");
                dialogueText.text = currElement.dialogueText;
                yield return new WaitForSeconds(0.5f);
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
        ChangeEnabledStateOfUiObjects(false);
    }

    IEnumerator PrintDialogueItem(DialogueElement de)
    {
        dialogeFinishedPrinting = false;
        int j = 0;
        while(j < de.dialogueText.Length)
        {
            dialogueText.text += de.dialogueText[j];
            j++;
            yield return new WaitForSeconds(de.playbackSpeed);
        }
        dialogeFinishedPrinting = true;
    }

    private void ChangeEnabledStateOfUiObjects(bool state)
    {
        speakerLeft.enabled = state;
        speakerRight.enabled = state;
        dialogueText.enabled = state;
    }
}
