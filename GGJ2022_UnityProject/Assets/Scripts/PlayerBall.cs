using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBall : MonoBehaviour
{
    GameManager gameManager;
    MalusManager malusManager;

    [SerializeField] GameObject dashState;
    [SerializeField] GameObject ball;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        malusManager = FindObjectOfType<MalusManager>();
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
    }

    private void OnDisable()
    {
        malusManager.EndMalus();
    }

    private void Update()
    {
        float alpha = Time.deltaTime / gameManager.winDuration;

        if (transform.parent.GetComponent<PlayerControls>().playerNumber == PlayerNumber.PlayerOne && gameManager.playerOneScore < 100f)
        {
            gameManager.playerOneScore += alpha * 100;
        }
        else if ((transform.parent.GetComponent<PlayerControls>().playerNumber == PlayerNumber.PlayerTwo && gameManager.playerTwoScore < 100f))
        {
            gameManager.playerTwoScore += alpha * 100;
        }

        gameManager.UpdateText();
    }
}
