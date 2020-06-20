using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    public Image fadeOutBlack;

    private bool fadeOutAndIn = false;
    private float secondsBetween = 0;

    public void SetBlackAlphaLevel(float level)
    {
        Color c = fadeOutBlack.color;

        if(level > 255)
        {
            level = 255;
        }
        if(level < 0)
        {
            level = 0;
        }

        fadeOutBlack.color = new Color(c.r, c.g, c.b, level);
    }

    public void FadeOutScreen(float fadeOutTick)
    {
        StartCoroutine("FadeOutToBlack", fadeOutTick);
    }

    public void FadeInScreen(float fadeOutTick)
    {
        StartCoroutine("FadeInFromBlack", fadeOutTick);
    }

    IEnumerator FadeOutToBlack(float fadeOutTick)
    {
        float alpha = fadeOutBlack.color.a;
        Color staticBlack = fadeOutBlack.color;
        while (alpha <= 1)
        {
            fadeOutBlack.color = new Color(staticBlack.r, staticBlack.g, staticBlack.b, alpha);
            alpha += 0.01f;
            yield return new WaitForSeconds(fadeOutTick);
        }

        if(fadeOutAndIn)
        {
            StartCoroutine("WaitInBetween", fadeOutTick);
        }
    }

    IEnumerator FadeInFromBlack(float fadeOutTick)
    {
        float alpha = fadeOutBlack.color.a;
        Color staticBlack = fadeOutBlack.color;
        while (alpha >= 0)
        {
            fadeOutBlack.color = new Color(staticBlack.r, staticBlack.g, staticBlack.b, alpha);
            alpha -= 0.01f;
            yield return new WaitForSeconds(fadeOutTick);
        }

        if (fadeOutAndIn)
        {
            fadeOutAndIn = false;
        }
    }

    public void FadeOutAndIn(float secondsBetween, float fadeOutTick = 0.1f)
    {
        fadeOutAndIn = true;
        this.secondsBetween = secondsBetween;
        FadeOutScreen(fadeOutTick);
    }

    IEnumerator WaitInBetween(float fadeOutTick)
    {
        do
        {
            yield return new WaitForSeconds(secondsBetween);
        }while (false) ;

        StartCoroutine("FadeInFromBlack", fadeOutTick);
    }
}
