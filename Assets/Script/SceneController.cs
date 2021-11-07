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
        SceneManager.LoadScene("Level1");
    }

    /*
    public void PilihLevel2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void PilihLevel3()
    {
        SceneManager.LoadScene("Level3");
    }
    */
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
        SceneManager.LoadScene("Level1");
    }
    /*
    public void RestartLv2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void RestartLv3()
    {
        SceneManager.LoadScene("Level3");
    }
    */
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToScene(string nama)
    {
         SceneManager.LoadScene(nama);
    }
}
