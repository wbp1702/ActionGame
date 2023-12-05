using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    public void OnPlayButton()
    {
        SceneManager.LoadScene(2);
    }
    public void OnMenuButton()
    {
        SceneManager.LoadScene(0);
    }
    public void OnQuitButton()
    {
        Application.Quit();
    }
}
