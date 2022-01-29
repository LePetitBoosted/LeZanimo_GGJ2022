using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBall : MonoBehaviour
{
    [SerializeField] GameObject dashState;
    [SerializeField] GameObject ball;
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
}
