using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Script : MonoBehaviour
{
    public void mainMenu()
    {
        ServiceLocator.SceneManager.LoadSceneByName("MainMenu");
    }

    public void StartGame()
    {
        ServiceLocator.SceneManager.LoadSceneByName("GameScene");
    }

    public void winScene()
    {
        ServiceLocator.SceneManager.LoadSceneByName("WinScene");
    }

    public void loseScene()
    {
        ServiceLocator.SceneManager.LoadSceneByName("LoseScene");
    }

    public void options()
    {
        ServiceLocator.SceneManager.LoadSceneByName("Options");
    }

    public void howToPlay()
    {
        ServiceLocator.SceneManager.LoadSceneByName("HowToPlay");
    }
}
