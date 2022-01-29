using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckOne : MonoBehaviour
{
    bool isGrounded;

    PlayerOneControls playerControls;

    private void Awake()
    {
        playerControls = transform.parent.GetComponent<PlayerOneControls>();
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
