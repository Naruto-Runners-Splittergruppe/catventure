using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueHandler : MonoBehaviour
{
    private Dialogue dialogue;
    [Header("UI Elements")]
    public Text dialogueText;
    public Image speakerLeft;
    public Image speakerRight;
    public Image multiDialogueBackground;
    public Text multiOption1;
    public Text multiOption2;
    public Text multiOption3;
    public string choisePointer = "> ";
    public Text upwardsArrow;
    public Text downwardsArrow;
    [Header("----------")]
    public string language = "english";
    public event EventHandler dialogueEndedEventHandler;

    private bool dialogeFinishedPrinting;
    private bool isInMultiChoise;
    private List<Text> multiOptions;

    private void Start()
    {
        multiOptions = new List<Text>();
        multiOptions.Add(multiOption1);
        multiOptions.Add(multiOption2);
        multiOptions.Add(multiOption3);
    }

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

            if (currElement.multiChoiceDialogue.Count > 0)
            {
                isInMultiChoise = true;
                SetMultiChoiceDialogue(currElement.multiChoiceDialogue);
                while (isInMultiChoise)
                {
                    yield return null;
                }
            }

            i++;
            if (i >= dialogue.dialogueItems.Count)
            {
                dialogueHasEnded = true;
            }
        }
        dialogueEndedEventHandler.Invoke(this, null);
        ChangeEnabledStateOfUiObjects(false);
    }

    IEnumerator PrintDialogueItem(DialogueElement elem)
    {
        string text = GetDialogueTextInCurrentLanguage(elem);
        dialogeFinishedPrinting = false;
        int j = 0;
        while (j < text.Length)
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
        if (language == "german")
        {
            return elem.dialogueTextGerman;
        }
        else
        {
            return elem.dialogueTextEnglish;
        }
    }

    private void SetMultiChoiceDialogue(List<MultiChoiceDialogue> mcd)
    {
        multiDialogueBackground.enabled = true;

        if (mcd.Count > 3)
        {
            downwardsArrow.enabled = true;
        }

        if (mcd.Count >= 1)
        {
            multiOption1.text = choisePointer + mcd[0].dialogueOption;
            multiOption1.enabled = true;
            multiOption1.color = Color.green;
        }
        if (mcd.Count >= 2)
        {
            multiOption2.text = choisePointer + mcd[1].dialogueOption;
            multiOption2.enabled = true;
        }
        if (mcd.Count >= 3)
        {
            multiOption3.text = choisePointer + mcd[2].dialogueOption;
            multiOption3.enabled = true;
        }
        StartCoroutine("WaitForPlayerToChoose", mcd);
    }

    private IEnumerator WaitForPlayerToChoose(List<MultiChoiceDialogue> mcd)
    {
        int currentText = 0;
        int forwardMoves = 0;
        int currentUiOption = 0;
        Text selectedItem = multiOptions[0];
        yield return new WaitForSeconds(0.1f);
        while (!Input.GetButtonDown("Interact"))
        {
            if (Input.GetAxis("Vertical") != 0)
            {
                if (Input.GetAxis("Vertical") < 0 && currentText+1 < mcd.Count)
                {
                    currentUiOption++;
                    if (currentUiOption == 3 && forwardMoves + multiOptions.Count < mcd.Count)
                    {
                        currentText = MoveChoises(mcd, currentText);
                        forwardMoves++;
                    }
                    else
                    {
                        currentText++;
                    }
                    if (currentUiOption > 2)
                    {
                        currentUiOption = 2;
                    }
                }
                else if (Input.GetAxis("Vertical") > 0 && currentText - 1 >= 0)
                {
                    currentUiOption--;
                    if (currentUiOption == -1 && forwardMoves > 0)
                    {
                        upwardsArrow.enabled = true;
                        currentText = MoveChoises(mcd, currentText, false);
                        forwardMoves--;
                    }
                    else
                    {
                        currentText--;
                    }
                    if (currentUiOption < 0)
                    {
                        currentUiOption = 0;
                    }
                }
                selectedItem.color = Color.black;
                selectedItem = multiOptions[currentUiOption];
                selectedItem.color = Color.green;

                if(currentText == 0)
                {
                    upwardsArrow.enabled = false;
                }
                if(currentText == mcd.Count-1)
                {
                    downwardsArrow.enabled = false;
                }

                yield return new WaitForSeconds(0.2f);
            }
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        multiDialogueBackground.enabled = false;
        isInMultiChoise = false;
        multiOption1.enabled = false;
        multiOption2.enabled = false;
        multiOption3.enabled = false;
    }

    private int MoveChoises(List<MultiChoiceDialogue> mcd, int currentTextCounter, bool forward = true)
    {
        //move choises downwards
        if (currentTextCounter + 1 < mcd.Count && forward)
        {
            currentTextCounter++;
            multiOption1.text = multiOption2.text;
            multiOption2.text = multiOption3.text;
            multiOption3.text = choisePointer + mcd[currentTextCounter].dialogueOption;
            upwardsArrow.enabled = true;
        }
        //move Choises upwards
        else if (!forward && currentTextCounter - 1 >= 0)
        {
            currentTextCounter--;
            multiOption3.text = multiOption2.text;
            multiOption2.text = multiOption1.text;
            multiOption1.text = choisePointer + mcd[currentTextCounter].dialogueOption;
            downwardsArrow.enabled = true;
        }
        return currentTextCounter;
    }
}
