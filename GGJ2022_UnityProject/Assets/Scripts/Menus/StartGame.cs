using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    DataSaver dataSaver;

    private void Awake()
    {
        dataSaver = FindObjectOfType<DataSaver>();
    }

    public void StartTheGame() 
    {
        SceneManager.LoadScene("SN_Main");
    }

    public void Quit() 
    {
        Application.Quit();
    }

    public void OnAllStarChange(bool isOn) 
    {
        dataSaver.allStar = isOn;
    }
}
