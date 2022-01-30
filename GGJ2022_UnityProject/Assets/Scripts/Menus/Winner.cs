using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Winner : MonoBehaviour
{
    DataSaver dataSaver;

    [SerializeField] TMP_Text winnerText;

    private void Awake()
    {
        if (FindObjectOfType<DataSaver>() != null)
        {
            dataSaver = FindObjectOfType<DataSaver>();
        }
        SetText();
    }

    void SetText() 
    {
        if(dataSaver.winner == PlayerNumber.PlayerOne) 
        {
            winnerText.text = "RED PLAYER WINS";
            winnerText.color = Color.red;
        }
        else if (dataSaver.winner == PlayerNumber.PlayerTwo)
        {
            winnerText.text = "GREEN PLAYER WINS";
            winnerText.color = Color.green;
        }
    }

    public void MainMenu() 
    {
        SceneManager.LoadScene("SN_Menu");
    }
}
