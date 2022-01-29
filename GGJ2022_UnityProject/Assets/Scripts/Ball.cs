using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float ballSpeed;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        Vector2 firstMove = new Vector2(Random.Range(0.01f, 1f), Random.Range(0.01f, 1f));
        rb.AddForce(firstMove * 1000f, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude != ballSpeed)
        {
            rb.velocity = rb.velocity.normalized * ballSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 3) 
        {
            collision.gameObject.GetComponent<PlayerControls>().CatchBall();
            gameObject.SetActive(false);
        }
    }
}
