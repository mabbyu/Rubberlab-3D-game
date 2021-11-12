using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    public string nameScene;

    public GameObject menuPanel;
    public GameObject levelPanel;
    public GameObject optionPanel;
    public GameObject creditPanel;

    void Start()
    {
        menuPanel.SetActive(true);
        levelPanel.SetActive(false);
        optionPanel.SetActive(false);
        creditPanel.SetActive(false);
    }

    public void backMenu()
    {
        menuPanel.SetActive(true);
        levelPanel.SetActive(false);
        optionPanel.SetActive(false);
        creditPanel.SetActive(false);
    }

    public void PilihLevel()
    {
        menuPanel.SetActive(false);
        levelPanel.SetActive(true);
        optionPanel.SetActive(false);
        creditPanel.SetActive(false);
    }

    public void PilihLevel1()
    {
        SceneManager.LoadScene("1");
    }

    
    public void PilihLevel2()
    {
        SceneManager.LoadScene("2");
    }
    
    public void PilihLevel3()
    {
        SceneManager.LoadScene("3");
    }
    
    public void OptionMenu()
    {
        //SceneManager.LoadScene("Option");
        menuPanel.SetActive(false);
        levelPanel.SetActive(false);
        optionPanel.SetActive(true);
        creditPanel.SetActive(false);
    }

    public void CreditMenu()
    {
        //SceneManager.LoadScene("Credit");
        menuPanel.SetActive(false);
        levelPanel.SetActive(false);
        optionPanel.SetActive(false);
        creditPanel.SetActive(true);
    }

    public void RestartLv1()
    {
        SceneManager.LoadScene("1");
    }
    
    public void RestartLv2()
    {
        SceneManager.LoadScene("2");
    }
    
    public void RestartLv3()
    {
        SceneManager.LoadScene("3");
    }

    public void CutScene1()
    {
        SceneManager.LoadScene("CutScene1");
    }

    public void CutScene2()
    {
        SceneManager.LoadScene("CutScene2");
    }

    public void CutScene3()
    {
        SceneManager.LoadScene("CutScene3");
    }

    public void CutSceneEnd()
    {
        SceneManager.LoadScene("CutSceneEnd");
    }


    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Loading()
    {
        SceneManager.LoadScene("Loading");
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}