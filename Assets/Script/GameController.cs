using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public bool GameActived = true;

    public static bool isPaused;

    public GameObject mainPanel;
    public GameObject pausePanel;
    public GameObject optionPanel;

    public GameObject winPanel1;
    public GameObject winPanel2;
    public GameObject winPanel3;

    public GameObject losePanel;
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

    void Update()
    {
        //audio
        AudioListener.volume = audioGame.value;


        //health
        healthText.text = (playerHealth.currentHealth / 10).ToString();
        

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
        if(GameActived)
        {
            Time.timeScale = 1;
            waktu += Time.deltaTime;
            if(waktu >= 1)
            {
                time--;
                waktu = 0;
            }
            if (time <= 11)
            {
                if(!lastSecond.isPlaying)
                lastSecond.Play();
            }
        TextTime();
        }
        else
        {
            Time.timeScale = 0;

        }
        
        //score
        if(GameActived && time <= 0 && currScore >= 700 )
        {
            lastSecond.Stop();
            //LoseGame();
            WinGame();
        }

        if (GameActived && time <= 0 && currScore >= 500)
        {
            lastSecond.Stop();
            //LoseGame();
            WinGame2();
        }

        if (GameActived && time <= 0 && currScore >= 300)
        {
            lastSecond.Stop();
            //LoseGame();
            WinGame3();
        }

        if (GameActived && time <= 0 && currScore <= 300)
        {
            lastSecond.Stop();
            LoseGame();
            //WinGame();
        }

        //pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
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

    public void WinGame()
    {
        GameActived = false;
        winPanel3.SetActive(true);
        mainPanel.SetActive(false);
        pausePanel.SetActive(false);
        isPaused = true;

        if (!timerEnd.isPlaying)
        timerEnd.Play();
    }

    public void WinGame2()
    {
        GameActived = false;
        winPanel2.SetActive(true);
        mainPanel.SetActive(false);
        pausePanel.SetActive(false);
        isPaused = true;

        if (!timerEnd.isPlaying)
            timerEnd.Play();
    }

    public void WinGame3()
    {
        GameActived = false;
        winPanel1.SetActive(true);
        mainPanel.SetActive(false);
        pausePanel.SetActive(false);
        isPaused = true;

        if (!timerEnd.isPlaying)
            timerEnd.Play();
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

    public void BackButton()
    {
        PauseGame();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
