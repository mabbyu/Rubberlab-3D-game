using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {

    }

    //ke main menu
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    //ke setting
    public void Setting()
    {
        SceneManager.LoadScene("Setting");
    }

    //ke credit
    public void Credit()
    {
        SceneManager.LoadScene("Credit");
    }

    //keluar
    public void Exit()
    {
        Application.Quit();
    }

    //in game
    public void LevelKelas()
    {
        SceneManager.LoadScene("Kelas");
    }
}
