using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class IntroActions : CutsceneActions
{
    private static Vector2 originalPlayerPos = player.transform.position;

    public static void ResetPlayer()
    {
        player.transform.position = originalPlayerPos;
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

    #region EndOfLifes

    private static GameObject lucimau;

    public static void SpawnInLufimau()
    {
        lucimau = Instantiate(Resources.Load("NPCs/Lucimau"), new Vector3(-1.47f, 5.685f,0), new Quaternion(0,180,0,0)) as GameObject;

        MethodeEndActions();
    }

    public static void RotatePlayer()
    {
        Quaternion playerRotation = player.transform.rotation;

        if (playerRotation.y == 0)
        {
            player.transform.rotation = new Quaternion(playerRotation.x, 180, playerRotation.z, playerRotation.w);
        }
        else
        {
            player.transform.rotation = new Quaternion(playerRotation.x, 0, playerRotation.z, playerRotation.w);
        }

        MethodeEndActions();
    }

    #endregion
}
