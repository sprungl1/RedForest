using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour

{
    // Called when we click the "Play" button.
    public void OnPlayButton()
    {
        SceneManager.LoadScene(1);
    }
    // Called when we click the "Quit" button.
    public void OnQuitButton()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }
    public void OnBackToMainMenuButton()
    {
        SceneManager.LoadScene(0);
    }
}