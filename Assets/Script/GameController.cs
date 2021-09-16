using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Text scoreText;
    public Text timeText;

    public float score;

    public float timer;

    public GameObject mainPanel;

    public GameObject winPanel;

    public GameObject losePanel;
    
    public GameObject pausePanel;
    public static bool GameIsPaused = false;

    public static GameController instance;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        timeText.text = timer.ToString("N0");
        if (timer <= 0)
        {
            losePanel.SetActive(true);
            Time.timeScale = 0f;
            mainPanel.SetActive(false);
        }
        else
        {
            timer -= Time.deltaTime;
        }

        scoreText.text = score.ToString();
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        mainPanel.SetActive(true);
        Debug.Log("Resume game");
    }

    void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        mainPanel.SetActive(false);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Debug.Log("main menu");
        Time.timeScale = 1f;
    }
}