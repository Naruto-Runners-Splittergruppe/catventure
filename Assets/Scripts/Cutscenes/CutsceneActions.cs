using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneActions : MonoBehaviour
{
    private const float DEFAULT_FADE = 0.005f;

    protected static GameObject player;
    protected static Rigidbody2D rb2dPlayer;
    protected static FadeOut fadeOut;

    public static bool CallFinished { get; set; }
    public static EventHandler CallFinishedEventHandler { get; set; }

    static CutsceneActions()
    {
        InizializeGameObjects();
    }

    protected static void InizializeGameObjects()
    {
        CallFinished = false;
        player = GameObject.FindGameObjectWithTag("Player");
        rb2dPlayer = player.GetComponent<Rigidbody2D>();
        fadeOut = GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<FadeOut>();
    }

    public static void FadeOut(float fadeOutTick)
    {
        fadeOut.FadeOutScreen(fadeOutTick);
        CallFinished = true;
    }

    public static void FadeOutDefault()
    {
        fadeOut.FadeOutScreen(DEFAULT_FADE);
        CallFinished = true;
    }

    public static void FadeIn(float fadeOutTick)
    {
        fadeOut.FadeInScreen(fadeOutTick);
        CallFinished = true;
    }

    public static void FadeInDefault()
    {
        fadeOut.FadeInScreen(DEFAULT_FADE);
        CallFinished = true;
    }

    public static void FadeOutAndInDefault()
    {
        fadeOut.FadeOutAndIn(2, DEFAULT_FADE);
        CallFinished = true;
    }

    public static void LoadLevel(object levelId)
    {
        int id = int.Parse((string) levelId);
        SceneManager.LoadScene(id);
    }
}
