using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextSceneOnCol : MonoBehaviour
{
    private int sceneIndex = 2;
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "Snowball")
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
