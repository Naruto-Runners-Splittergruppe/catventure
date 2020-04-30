using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class DialogueElement
{
    public enum AvatarPos { left, right};
    public string speakerName;
    public AvatarPos speakerPosition;
    public Sprite speakerPic;
    public string dialogueText;
    public GUIStyle dialogueFont;
    public float playbackSpeed;
    public AudioClip soundForTextScrolling;
    public string nameOfMethodeToActivateBeforeNextText;
    public bool waitUntilMethodeIsFinished;
    public bool isMultichoice;
    [Header("Is only used, if isMultichoice is true")]
    public MultiChoiceDialogue multiChoiceDialogue;
}

