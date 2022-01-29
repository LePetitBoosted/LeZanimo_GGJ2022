using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    bool isGrounded;

    PlayerControls playerControls;

    private void Awake()
    {
        playerControls = transform.parent.GetComponent<PlayerControls>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6) 
        {
            isGrounded = true;
            SetIsGrounded();
            playerControls.CheckBunnyJump();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            isGrounded = false;
            SetIsGrounded();
        }
    }

    void SetIsGrounded() 
    {
        playerControls.isGrounded = isGrounded;
        playerControls.SetAirControl();
    }
}
