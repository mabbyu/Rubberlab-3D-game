using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene : MonoBehaviour
{
    public GameObject cutScene1;
    public GameObject cutScene2;
    public GameObject cutScene3;
    public GameObject hintPanel;


    void Start()
    {
        cutScene1.SetActive(true);
    }

    public void Panel1()
    {
        cutScene1.SetActive(true);
    }

    public void Panel2()
    {
        cutScene2.SetActive(true);
    }

    public void Panel3()
    {
        cutScene3.SetActive(true);
    }

    public void HintPanel()
    {
        hintPanel.SetActive(true);
    }
}
