using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    DataSaver dataSaver;
    [SerializeField] GameObject dataSaverToInstantiate;

    private void Awake()
    {
        if(FindObjectOfType<DataSaver>() == null) 
        {
            GameObject tempDataSaver = Instantiate(dataSaverToInstantiate);
            dataSaver = tempDataSaver.GetComponent<DataSaver>();
        }
        else 
        {
            dataSaver = FindObjectOfType<DataSaver>();
        }
    }

    public void StartTheGame() 
    {
        dataSaver.allStar = false;
        SceneManager.LoadScene("SN_Main");
    }

    public void StartAllStar() 
    {
        dataSaver.allStar = true;
        SceneManager.LoadScene("SN_Main");
    }

    public void Quit() 
    {
        Application.Quit();
    }

}
