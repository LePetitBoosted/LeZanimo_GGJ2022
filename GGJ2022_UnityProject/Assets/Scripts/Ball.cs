using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float ballSpeed;
    bool canBeCatch;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        StartCoroutine(CatchDelay());

        Vector2 firstMove = new Vector2(Random.Range(0.01f, 1f), Random.Range(0.01f, 1f));
        rb.AddForce(firstMove * ballSpeed, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude != ballSpeed)
        {
            rb.velocity = rb.velocity.normalized * ballSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 3 && canBeCatch == true) 
        {
            canBeCatch = false;
            col.transform.parent.GetComponent<PlayerControls>().CatchBall();
            gameObject.SetActive(false);
        }
    }

    IEnumerator CatchDelay() 
    {
        canBeCatch = false;
        GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);

        yield return new WaitForSeconds(2f);

        canBeCatch = true;
        GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
    }
}
