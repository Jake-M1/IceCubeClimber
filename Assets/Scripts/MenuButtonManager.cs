using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonManager : MonoBehaviour
{

    //Manages Levels to Load

    //Starts Game
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    //Closes Application
    public void QuitGame()
    {
        Application.Quit();
    }

    //Loads Main Menu
    public void QuitToMainMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    //Code Written By Kurt Santos
}

