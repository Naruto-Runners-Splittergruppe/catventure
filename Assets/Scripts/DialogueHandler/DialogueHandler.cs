using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class DialogueHandler : MonoBehaviour
{
    private Dialogue dialogue;
    [Header("UI Elements")]
    public Text dialogueText;
    public Text speakerName;
    public Image dialogueBackground;
    public Image speakerLeft;
    public Image speakerRight;
    public Image multiDialogueBackground;
    public Text multiOption1;
    public Text multiOption2;
    public Text multiOption3;
    public string choisePointer = "> ";
    public Text upwardsArrow;
    public Text downwardsArrow;
    public Image eButton;
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

    public void StartDialogue(Dialogue dialogue, int startAt = 0)
    {
        if(startAt > dialogue.dialogueItems.Count)
        {
            Debug.LogError("startAt was to big. Dialogue has only: " + dialogue.dialogueItems.Count + " items.");
            startAt = 0;
        }

        this.dialogue = dialogue;
        if (dialogueText != null && speakerLeft != null && speakerRight != null)
        {
            StartCoroutine("PrintDialogue", startAt);
        }
        else
        {
            Debug.LogError("UI elements not set correctly.");
        }
    }

    IEnumerator PrintDialogue(int startAt)
    {
        bool dialogueHasEnded = false;
        int i = startAt;

        EnableBasicDialogue();
        while (!dialogueHasEnded)
        {
            DialogueElement currElement = dialogue.dialogueItems[i];
            if (!currElement.justCallMethode)
            {
                EnableBasicDialogue(false);
                string currentText = GetDialogueTextInCurrentLanguage(currElement);
                speakerName.text = currElement.speakerName;

                if (currElement.speakerPosition == DialogueElement.AvatarPos.left)
                {
                    var rotation = speakerLeft.transform.rotation;
                    speakerLeft.enabled = true;
                    speakerLeft.sprite = currElement.speakerPic;

                    if (currElement.flipSpeakerPic)
                    {
                        speakerLeft.transform.rotation = new Quaternion(rotation.x, 180, rotation.z, rotation.w);
                    }
                    else
                    {
                        speakerLeft.transform.rotation = new Quaternion(rotation.x, 0, rotation.z, rotation.w);
                    }
                }
                else if (currElement.speakerPosition == DialogueElement.AvatarPos.right)
                {
                    var rotation = speakerRight.transform.rotation;
                    speakerRight.enabled = true;
                    speakerRight.sprite = currElement.speakerPic;

                    if (currElement.flipSpeakerPic)
                    {
                        speakerRight.transform.rotation = new Quaternion(rotation.x, 180, rotation.z, rotation.w);
                    }
                    else
                    {
                        speakerRight.transform.rotation = new Quaternion(rotation.x, 0, rotation.z, rotation.w);
                    }
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
            }

            //Call a methode before the next dialogue element
            if (currElement.nameOfMethodeToActivateBeforeNextText != "")
            {
                DisableCompleteDialogeBox();
                Type t = CallMethode(currElement.nameOfMethodeToActivateBeforeNextText);

                if (currElement.waitUntilMethodeIsFinished && t != null)
                {
                    PropertyInfo pI;

                    if (t != typeof(CutsceneActions))
                    {
                        pI = t.BaseType.GetProperty("CallFinished");
                    }
                    else
                    {
                        pI = t.GetProperty("CallFinished");
                    }

                    if (pI.PropertyType == typeof(bool))
                    {
                        while (!(bool)pI.GetValue(null))
                        {
                            yield return new WaitForEndOfFrame();
                        }

                        pI.SetValue(null,false); 
                    }
                    else
                    {
                        Debug.LogError("CallFinished of Property: " + pI.Name + " has the wrong Type. Must be bool.");
                    }
                }

                do
                {
                    yield return new WaitForSeconds(currElement.waitForSecondsAfterMethodeFinished);
                } while (false);
            }

            i++;
            if (i >= dialogue.dialogueItems.Count)
            {
                dialogueHasEnded = true;
            }
        }

        if (dialogueEndedEventHandler != null)
        {
            dialogueEndedEventHandler.Invoke(this, null);
        }

        DisableCompleteDialogeBox();
    }

    private Type CallMethode(string classMethodeString)
    {
        string[] classMethodeSeperated = classMethodeString.Split('.');
        if (classMethodeSeperated.Length != 2)
        {
            Debug.LogError("The String: " + classMethodeString + "is no propper Class-Methode String. The Methode can't be executed. If you are using floats please use a _ instead of a .");
            return null;
        }

        string methodeName = "";
        object[] parameters = null;

        if (classMethodeSeperated[1].Contains("(") && classMethodeSeperated[1].Contains(")"))
        {
            string[] splittedParams = classMethodeSeperated[1].Split('(');
            methodeName = splittedParams[0];
            splittedParams[1] = splittedParams[1].Replace('_', '.');
            parameters = splittedParams[1].TrimStart('(').TrimEnd(')', ';').Split(',');
        }
        else
        {
            methodeName = classMethodeSeperated[1].Split('(')[0];
        }

        Type t = Type.GetType(classMethodeSeperated[0]);
        MethodInfo mI = t.GetMethod(methodeName);

        if(mI == null)
        {
            Debug.LogError("Couldn't find methode name: " + methodeName);
            return null;
        }

        if (parameters == null || parameters.Length == 0 || (parameters is string[] && parameters.Length == 1 && ((string)parameters[0]).Equals("")))
        {
            mI.Invoke(null, null);
        }
        else
        {
            mI.Invoke(null, parameters);
        }

        return t;
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

    private void ChangeEnabledStateOfChangingUiObjects(bool state)
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
                if (Input.GetAxis("Vertical") < 0 && currentText + 1 < mcd.Count)
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

                if (currentText == 0)
                {
                    upwardsArrow.enabled = false;
                }
                if (currentText == mcd.Count - 1)
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

    private void DisableCompleteDialogeBox()
    {
        speakerRight.enabled = false;
        speakerLeft.enabled = false;
        dialogueText.enabled = false;
        multiDialogueBackground.enabled = false;
        multiOption1.enabled = false;
        multiOption2.enabled = false;
        multiOption3.enabled = false;
        upwardsArrow.enabled = false;
        downwardsArrow.enabled = false;
        dialogueBackground.enabled = false;
        eButton.enabled = false;
        speakerName.enabled = false;
    }

    private void EnableBasicDialogue(bool clean = true)
    {
        if(clean)
        {
            dialogueText.text = "";
            multiOption1.text = "";
            multiOption2.text = "";
            multiOption3.text = "";
            speakerName.text = "";
        }
        
        dialogueText.enabled = true;
        eButton.enabled = true;
        dialogueBackground.enabled = true;
        speakerName.enabled = true;
    }
}
