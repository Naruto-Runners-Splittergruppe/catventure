using System;
using UnityEditor;
using UnityEngine;

public class DialogueAsset
{
    [MenuItem("Assets/Create/Dialogue")]
    public static void CreateAsset()
    {
        CustomAssetUtility.CreateAsset<Dialogue>();
    }
}
