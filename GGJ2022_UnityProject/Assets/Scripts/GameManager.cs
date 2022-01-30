using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float winDuration;

    public float playerOneScore = 0;
    public float playerTwoScore = 0;

    [SerializeField] TMP_Text playerOneScoreText;
    [SerializeField] TMP_Text playerTwoScoreText;

    [SerializeField] Image playerOneFillBar;
    [SerializeField] Image playerTwoFillBar;

    private void Awake()
    {
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
}