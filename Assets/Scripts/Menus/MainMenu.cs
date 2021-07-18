using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private string keyboard = "KEYBOARD";
    private string mouse = "MOUSE + KEYBOARD";
    public Text controlText;

    public static MainMenu Instance;
    private void Awake()
    {
        Instance = this;
        controlText.text = mouse;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ChangeControl()
    {
        if (controlText.text == keyboard)
        {
            controlText.text = mouse;
        }
        else if (controlText.text == mouse)
        {
            controlText.text = keyboard;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
