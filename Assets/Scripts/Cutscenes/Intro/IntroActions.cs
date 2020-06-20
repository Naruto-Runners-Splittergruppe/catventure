using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class IntroActions : CutsceneActions
{
    private static Vector2 playerPos = player.transform.position;

    public static void ResetPlayer()
    {
        player.transform.position = playerPos;
    }

    public static void MethodeEndActions()
    {
        CallFinished = true;
    }

    public static void PlayerClimpUp()
    {
        player.transform.position = new Vector2(-2.77f, 5.63f);

    }

    #region Life 1
    public static void PlayerJumpForIntroFirst(object speedMultiplStr)
    {
        float speedMultipl = float.Parse(speedMultiplStr as string, CultureInfo.InvariantCulture);
        InizializeGameObjects();

        rb2dPlayer.AddForce((Vector2.up + Vector2.right) * speedMultipl);

        MethodeEndActions();
    }

    #endregion

    #region Life 2

    public static void PrepareforSecondJumpPartOne()
    {
        rb2dPlayer.AddForce(Vector2.left * 200);

        MethodeEndActions();
    }

    public static void PrepareforSecondJumpPartTwo()
    {
        rb2dPlayer.AddForce(Vector2.right * 200);

        MethodeEndActions();
    }

    public static void PlayerJumpForIntroSecond(object speedMultiplStr)
    {
        float speedMultipl = float.Parse(speedMultiplStr as string, CultureInfo.InvariantCulture);
        InizializeGameObjects();
        rb2dPlayer.AddForce((Vector2.up + Vector2.right) * speedMultipl);

        MethodeEndActions();
    }

    #endregion
}
