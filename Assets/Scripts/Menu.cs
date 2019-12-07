using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
    public void MenuButton()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
    public void QuitButton()
    {
        Application.Quit();
    }
}
