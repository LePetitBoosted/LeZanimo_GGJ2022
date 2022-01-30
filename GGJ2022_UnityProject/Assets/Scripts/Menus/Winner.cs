using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Winner : MonoBehaviour
{
    DataSaver dataSaver;

    [SerializeField] Image winnerImage;
    [SerializeField] Sprite playerOneWinningScreen;
    [SerializeField] Sprite playerTwoWinningScreen;

    private void Awake()
    {
        if (FindObjectOfType<DataSaver>() != null)
        {
            dataSaver = FindObjectOfType<DataSaver>();
        }
        SetWinningScreen();
    }

    void SetWinningScreen() 
    {
        if(dataSaver.winner == PlayerNumber.PlayerOne) 
        {
            winnerImage.sprite = playerOneWinningScreen;
        }
        else if (dataSaver.winner == PlayerNumber.PlayerTwo)
        {
            winnerImage.sprite = playerTwoWinningScreen;
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause")) 
        {
            MainMenu();
        }
    }

    void MainMenu() 
    {
        SceneManager.LoadScene("SN_Menu");
    }
}
