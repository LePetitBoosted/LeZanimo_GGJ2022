using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBall : MonoBehaviour
{
    GameManager gameManager;
    MalusManager malusManager;
    VibrationManager vibrationManager;

    [SerializeField] GameObject dashState;
    [SerializeField] GameObject ball;
    [SerializeField] GameObject ballUI;

    public bool hasLifeBitchMalus;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        malusManager = FindObjectOfType<MalusManager>();
        vibrationManager = gameManager.gameObject.GetComponent<VibrationManager>();



        if (GetComponentInParent<PlayerControls>().playerNumber == PlayerNumber.PlayerOne)
        {
            vibrationManager.SetVibration(0.4f, 0.2f, PlayerNumber.PlayerOne);
        }
        else
        {
            vibrationManager.SetVibration(0.4f, 0.2f, PlayerNumber.PlayerTwo);
        }
    }

    public void LooseBall() 
    {
        if (dashState.activeSelf == false)
        {
            ball.transform.position = transform.position;
            ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Vector2 loseDirection = new Vector2(Random.Range(-0.25f, 0.25f), 1);
            ball.GetComponent<Rigidbody2D>().AddForce(loseDirection * 10f);
            ball.SetActive(true);

            gameObject.SetActive(false);
        }
    }

    public void LooseBallOnDeath()
    {
        ball.transform.position = transform.position;
        ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Vector2 loseDirection = new Vector2(Random.Range(-0.25f, 0.25f), 1);
        ball.GetComponent<Rigidbody2D>().AddForce(loseDirection * 10f);
        ball.SetActive(true);

        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        ballUI.SetActive(true);
        malusManager.GiveMalus(transform.parent.gameObject);
        hasLifeBitchMalus = false;
    }

    private void OnDisable()
    {
        
        if (ballUI != null)
        {
            ballUI.SetActive(false);
        }
        malusManager.EndMalus();

    }

    private void Update()
    {
        float alpha = Time.deltaTime / gameManager.winDuration;

        if (hasLifeBitchMalus == false)
        {
            if (transform.parent.GetComponent<PlayerControls>().playerNumber == PlayerNumber.PlayerOne && gameManager.playerOneScore < 100f)
            {
                gameManager.playerOneScore += alpha * 100;
            }
            else if ((transform.parent.GetComponent<PlayerControls>().playerNumber == PlayerNumber.PlayerTwo && gameManager.playerTwoScore < 100f))
            {
                gameManager.playerTwoScore += alpha * 100;
            }
        }
        else
        {
            if (transform.parent.GetComponent<PlayerControls>().playerNumber == PlayerNumber.PlayerOne && gameManager.playerOneScore > 0f)
            {
                gameManager.playerOneScore -= alpha * 100;
            }
            else if ((transform.parent.GetComponent<PlayerControls>().playerNumber == PlayerNumber.PlayerTwo && gameManager.playerTwoScore > 0f))
            {
                gameManager.playerTwoScore -= alpha * 100;
            }
        }

        gameManager.UpdateUI();
        gameManager.CheckForWin();
    }
}
