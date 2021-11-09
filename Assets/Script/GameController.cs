using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public bool GameActived = true;

    public static bool isPaused;

    public GameObject mainPanel;
    public GameObject pausePanel;
    public GameObject optionPanel;

    public GameObject winPanel;

    public GameObject bintang1;
    public GameObject bintang2;
    public GameObject bintang3;

    public GameObject losePanel;

    public GameObject upgradePanel;

    //public GameObject optionPanel;

    //hp player
    public Text healthText;
    public PlayerController playerHealth;

    //score
    public Text scoreText;
    public float currScore;

    public float maxScore;

    //timer
    public Text timerText;
    public float time;

    float waktu;

    public AudioSource lastSecond;
    public AudioSource timerEnd;

    //spawn guru
    public GameObject spawnGuru;

    public static GameController instance;


    //slider
    public Slider audioGame;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Data.currUpgrade = 1;
        Data.currLevel = SceneManager.GetActiveScene().name;
        if (Data.currLevel == "1")
        {
            Data.canSprint = false;
            Data.canDoubleJump = false;
        }
    }

    void Update()
    {
        //audio
        AudioListener.volume = audioGame.value;


        //health
        healthText.text = (playerHealth.currentHealth / 10).ToString("N0");


        //score
        scoreText.text = "SCORE: " + currScore.ToString();

        /*if (currScore >= maxScore)
        {
            WinGame();
        }*/

        if (currScore >= (maxScore / 2))
        {
            spawnGuru.SetActive(true);
        }
        else
        {
            spawnGuru.SetActive(false);
        }


        //timer
        if (GameActived)
        {
            Time.timeScale = 1;
            waktu += Time.deltaTime;
            if (waktu >= 1)
            {
                time--;
                waktu = 0;
            }
            if (time <= 11)
            {
                if (!lastSecond.isPlaying)
                    lastSecond.Play();
            }
            TextTime();
        }
        else
        {
            Time.timeScale = 0;

        }

        //score
        if (GameActived && time <= 0 && currScore >= 700)
        {
            lastSecond.Stop();
            GameActived = false;
            winPanel.SetActive(true);
            bintang3.SetActive(true);
            mainPanel.SetActive(false);
            pausePanel.SetActive(false);
            isPaused = true;

            if (!timerEnd.isPlaying)
                timerEnd.Play();
        }

        if (GameActived && time <= 0 && currScore >= 500)
        {
            lastSecond.Stop();
            GameActived = false;
            winPanel.SetActive(true);
            bintang2.SetActive(true);
            mainPanel.SetActive(false);
            pausePanel.SetActive(false);
            isPaused = true;

            if (!timerEnd.isPlaying)
                timerEnd.Play();
        }

        if (GameActived && time <= 0 && currScore >= 300)
        {
            lastSecond.Stop();
            GameActived = false;
            winPanel.SetActive(true);
            bintang1.SetActive(true);
            mainPanel.SetActive(false);
            pausePanel.SetActive(false);
            isPaused = true;

            if (!timerEnd.isPlaying)
                timerEnd.Play();
        }

        if (GameActived && time <= 0 && currScore <= 300)
        {
            lastSecond.Stop();
            LoseGame();
        }

        //pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        if (isPaused)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void TextTime()
    {
        int Minutes = Mathf.FloorToInt(time / 60);
        int Seconds = Mathf.FloorToInt(time % 60);
        timerText.text = Minutes.ToString("00") + ":" + Seconds.ToString("00");
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        mainPanel.SetActive(true);
        optionPanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        GameActived = true;
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        mainPanel.SetActive(false);
        optionPanel.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
        GameActived = false;
    }

    public void OptionGame()
    {
        pausePanel.SetActive(false);
        mainPanel.SetActive(false);
        optionPanel.SetActive(true);
    }

    public void LoseGame()
    {
        GameActived = false;
        losePanel.SetActive(true);
        mainPanel.SetActive(false);
        pausePanel.SetActive(false);
        isPaused = true;

        if (!timerEnd.isPlaying)
            timerEnd.Play();
    }

    public void UpgradePanel()
    {
        GameActived = false;

        upgradePanel.SetActive(true);

        winPanel.SetActive(false);
        losePanel.SetActive(false);

        mainPanel.SetActive(false);
        pausePanel.SetActive(false);
        isPaused = true;

        if (!timerEnd.isPlaying)
            timerEnd.Play();
    }

    public void BackFromUpgrade()
    {
        lastSecond.Stop();
        GameActived = false;
        winPanel.SetActive(true);
        mainPanel.SetActive(false);
        pausePanel.SetActive(false);
        isPaused = true;

        if (!timerEnd.isPlaying)
            timerEnd.Play();
    }

    public void UpgradeSpeed()
    {
        if (Data.currUpgrade > 0)
        {
            if (Data.canSprint == false)
            {
                Data.canSprint = true;
                Data.currUpgrade = 0;
            }
        }
    }

    public void UpgradeJump()
    {
        if (Data.currUpgrade > 0)
        {
            if (Data.canDoubleJump == false)
            {
                Data.canDoubleJump = true;
                Data.currUpgrade = 0;
            }
        }
    }

    public void BackButton()
    {
        PauseGame();
    }

    public void Quit()
    {
        Application.Quit();
    }  
}
