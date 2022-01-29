using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float airControl;
    [SerializeField] float dashForce;
    [SerializeField] float dashDuration;
    
    Rigidbody2D rb;

    float currentMoveSpeed;
    float horizontalMove;
    public bool isGrounded;
    public bool hasInput;
    Vector2 rawInputs;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentMoveSpeed = moveSpeed;
    }

    private void Update()
    {
        if (hasInput == true)
        {
            horizontalMove = Input.GetAxis("Horizontal");

            if (Input.GetButtonDown("Jump") && isGrounded) 
            {
                Jump();
            }

            if (Input.GetButtonDown("Dash"))
            {
                GetInputRaw();

                Vector2 dashDir;
                if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0 || Mathf.Abs(Input.GetAxis("Vertical")) > 0)
                {
                    if (Mathf.Abs(Input.GetAxis("Horizontal")) > Mathf.Abs(Input.GetAxis("Vertical")))
                    {
                        dashDir = new Vector2(rawInputs.x, 0);
                    }
                    else
                    {
                        dashDir = new Vector2(0, rawInputs.y);
                    }
                }
                else
                {
                    dashDir = new Vector2(1, 0);
                }
                StartCoroutine(Dash(dashDir));
            }
        }
    }

    private void FixedUpdate()
    {
        if (hasInput == true)
            rb.velocity = new Vector2((horizontalMove * currentMoveSpeed), rb.velocity.y);
    }

    void Jump() 
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

    IEnumerator Dash(Vector2 dashDirection) 
    {
        Debug.Log(dashDirection);

        hasInput = false;
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        rb.AddForce(dashDirection * dashForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(dashDuration);

        hasInput = true;
        rb.gravityScale = 1;
        rb.velocity = Vector2.zero;
    }

    public void CheckBunnyJump() 
    {
        if (hasInput == true)
        {
            if (Input.GetButton("Jump"))
            {
                Jump();
            }
        }
    }

    public void SetAirControl() 
    { 
        if(isGrounded == true) 
        {
            currentMoveSpeed = moveSpeed * 1f;
        }

        if (isGrounded == false)
        {
            currentMoveSpeed = moveSpeed * airControl;
        }
    }

    Vector2 GetInputRaw() 
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            if (Input.GetAxis("Horizontal") > 0)
                rawInputs.x = 1;
            if (Input.GetAxis("Horizontal") < 0)
                rawInputs.x = -1;
        }
        else
        {
            rawInputs.x = 0;
        }

        if (Input.GetAxis("Vertical") != 0)
        {
            if (Input.GetAxis("Vertical") > 0)
                rawInputs.y = 1;
            if (Input.GetAxis("Vertical") < 0)
                rawInputs.y = -1;
        }
        else
        {
            rawInputs.y = 0;
        }

        return rawInputs;
    }
}
