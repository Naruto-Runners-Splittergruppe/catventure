using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NotificationElement
{
    public string speakerName;
    public Sprite speakerPic;
    public string dialogueTextGerman;
    public string dialogueTextEnglish;
    public GUIStyle dialogueFont;
    public string nameOfMethodeToActivateBeforeNextText;
    public bool waitUntilMethodeIsFinished;
}
