using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class DialogueElement
{
    public enum AvatarPos { left, right};
    public string dialogueName;
    public string speakerName;
    public AvatarPos speakerPosition;
    public Sprite speakerPic;
    public bool flipSpeakerPic;
    public string dialogueTextGerman;
    public string dialogueTextEnglish;
    public GUIStyle dialogueFont;
    public float playbackSpeed = 0.05f;
    public AudioClip soundForTextScrolling;
    public string nameOfMethodeToActivateBeforeNextText;
    public bool waitUntilMethodeIsFinished;
    public bool justCallMethode;
    public float waitForSecondsAfterMethodeFinished = 0;
    public List<MultiChoiceDialogue> multiChoiceDialogue;
}

