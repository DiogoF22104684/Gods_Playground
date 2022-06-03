using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUtility : MonoBehaviour
{

    public void ExitApplication()
    {
        Application.Quit();
    }

    public void SceneTransition(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
