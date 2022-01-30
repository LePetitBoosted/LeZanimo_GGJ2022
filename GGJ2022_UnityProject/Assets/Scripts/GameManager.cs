using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    DataSaver dataSaver;

    public float winDuration;

    public float playerOneScore = 0;
    public float playerTwoScore = 0;

    [SerializeField] TMP_Text playerOneScoreText;
    [SerializeField] TMP_Text playerTwoScoreText;

    [SerializeField] Image playerOneFillBar;
    [SerializeField] Image playerTwoFillBar;

    private void Awake()
    {
        if (FindObjectOfType<DataSaver>() != null)
        {
            dataSaver = FindObjectOfType<DataSaver>();
        }
        UpdateUI();
    }

    public void UpdateUI() 
    {
        int playerOneScoreDisplayed = Mathf.RoundToInt(playerOneScore);
        int playerTwoScoreDisplayed = Mathf.RoundToInt(playerTwoScore);

        playerOneScoreText.text = playerOneScoreDisplayed + " / 100";
        playerTwoScoreText.text = playerTwoScoreDisplayed + " / 100";

        playerOneFillBar.fillAmount = playerOneScore / 100;
        playerTwoFillBar.fillAmount = playerTwoScore / 100;
    }

    void CheckForWin() 
    {
        if(playerOneScore == 100) 
        {
            StartCoroutine(EndGame(PlayerNumber.PlayerOne));
        }
        else if(playerTwoScore == 100) 
        {
            StartCoroutine(EndGame(PlayerNumber.PlayerTwo));
        }
    }

    IEnumerator EndGame(PlayerNumber winner) 
    {
        Time.timeScale = 0.3f;
        dataSaver.winner = winner;

        yield return new WaitForSeconds(1f);

        Time.timeScale = 1f;
        SceneManager.LoadScene("SN_Victory");
    }
}