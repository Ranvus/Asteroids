using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused = false;

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverMenu;

    private string keyboard = "KEYBOARD";
    private string mouse = "MOUSE + KEYBOARD";
    public Text controlText;

    public static PauseMenu Instance;
    private void Awake()
    {
        Instance = this;
        controlText.text = mouse;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    private void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1f;
    }

    public void GameOverMenu()
    {
        gameOverMenu.SetActive(true);
        Destroy(pauseMenu);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
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
