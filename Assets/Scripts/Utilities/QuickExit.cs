using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickExit : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if(Input.GetButtonDown("Cancel") && Debug.isDebugBuild)
        {
            Application.Quit();
        }
    }
}
