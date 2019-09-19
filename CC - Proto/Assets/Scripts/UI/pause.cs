using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UnityEngine;

public class pause : MonoBehaviour
{
    bool paused = false;

    void Update()
    {
        if (Input.GetKeyDown("escape"))
            paused = togglePause();
    }

    void OnGUI()
    {
        if (paused)
        {
            GUILayout.Label("Game is paused!");
            if (GUILayout.Button("Click me to unpause"))
                paused = togglePause();
            if (GUILayout.Button("Quit Game"))
                Application.Quit();
        }
    }



    bool togglePause()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            return (false);
        }
        else
        {
            Time.timeScale = 0f;
            return (true);
        }
    }
}
