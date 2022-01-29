using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float winDuration;

    public float playerOneScore = 0;
    public float playerTwoScore = 0;

    [SerializeField] TMP_Text playerOneScoreText;
    [SerializeField] TMP_Text playerTwoScoreText;

    private void Awake()
    {
        UpdateText();
    }

    public void UpdateText() 
    {
        int playerOneScoreDisplayed = Mathf.RoundToInt(playerOneScore);
        int playerTwoScoreDisplayed = Mathf.RoundToInt(playerTwoScore);

        playerOneScoreText.text = playerOneScoreDisplayed + " / 100";
        playerTwoScoreText.text = playerTwoScoreDisplayed + " / 100";
    }
}