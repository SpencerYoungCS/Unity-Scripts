using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "StartingScene")
            SceneManager.LoadScene("IntroScene");
        else
        {
            SceneManager.LoadScene("StartingScene");
        }
    }

}
