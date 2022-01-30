using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class PlayerBall : MonoBehaviour
{
    GameManager gameManager;
    MalusManager malusManager;

    [SerializeField] GameObject dashState;
    [SerializeField] GameObject ball;

    public bool hasLifeBitchMalus;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        malusManager = FindObjectOfType<MalusManager>();

        StartCoroutine(VibrationsOnSelf());
    }

    IEnumerator VibrationsOnSelf()
    {
        if (GetComponentInParent<PlayerControls>().playerNumber == PlayerNumber.PlayerOne)
        {
            GamePad.SetVibration(PlayerIndex.One, 0.4f, 0.4f);
            yield return new WaitForSeconds(0.2f);

            if (FindObjectOfType<HellVibrationsMalus>() != null)
            {
                GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
            }
        }
        else
        {
            GamePad.SetVibration(PlayerIndex.Two, 0.4f, 0.4f);
            yield return new WaitForSeconds(0.2f);

            if (FindObjectOfType<HellVibrationsMalus>() != null)
            {
                GamePad.SetVibration(PlayerIndex.Two, 0f, 0f);
            }
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

    private void OnEnable()
    {
        malusManager.GiveMalus(transform.parent.gameObject);
        hasLifeBitchMalus = false;
    }

    private void OnDisable()
    {
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
    }
}
