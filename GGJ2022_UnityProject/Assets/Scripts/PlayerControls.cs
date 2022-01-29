using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    
    Rigidbody2D rb;

    float horizontalMove;
    public bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2((horizontalMove * moveSpeed), rb.velocity.y);
    }

    void Jump() 
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

    public void CheckBunnyJump() 
    {
        if (Input.GetButton("Jump")) 
        {
            Jump();
        }
    }
}
