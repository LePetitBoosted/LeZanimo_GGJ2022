using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerNumber { PlayerOne, PlayerTwo };

public class PlayerControls : MonoBehaviour
{
    [Header("Set Player Number")]
    public PlayerNumber playerNumber;
    int playerID;
    public Joystick playerJoystick;

    DataManager dataManager;

    [Header("Player variables, change Data Manager instead!")]
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float airControl;
    [SerializeField] float dashForce;
    [SerializeField] float dashDuration;
    [SerializeField] float dashCooldownTime;


    Rigidbody2D rb;
    SpriteRenderer spriteRend;

    float currentMoveSpeed;
    float horizontalMove;
    [Header("Local variables")]
    public bool isGrounded;
    public bool hasInput;
    public bool dashAvailable;
    [SerializeField] bool isFacingRight;
    [SerializeField] GameObject normalState;
    [SerializeField] GameObject dashState;
    [SerializeField] GameObject playerBall;
    Vector2 rawInputs;

    float initialGravityScale;

    public float inputLag = 0;
    public string horizontalStr = "Horizontal P";
    public string verticalStr = "Vertical P";

    public Animator playerAnimator;
    public SoundManager soundManager;
    public AudioSource playerSound;

    private void Awake()
    {
        StartCoroutine(SetJoystick());

        dataManager = FindObjectOfType<DataManager>();
        rb = GetComponent<Rigidbody2D>();
        spriteRend = GetComponentInChildren<SpriteRenderer>();

        rb.mass = dataManager.mass;
        rb.drag = dataManager.linearDrag;
        rb.gravityScale = dataManager.gravityScale;

        moveSpeed = dataManager.moveSpeed;
        jumpForce = dataManager.jumpForce;
        airControl = dataManager.airControl;
        dashForce = dataManager.dashForce;
        dashDuration = dataManager.dashDuration;
        dashCooldownTime = dataManager.dashCooldownTime;
        
        currentMoveSpeed = moveSpeed;
        dashAvailable = true;
        initialGravityScale = rb.gravityScale;
    }

    private void Update()
    {
        if (hasInput == true && playerID != 0)
        {
            horizontalMove = Input.GetAxis(horizontalStr + playerID);

            if (horizontalMove > 0 && !isFacingRight)
            {
                Flip();
            }

            if (horizontalMove < 0 && isFacingRight)
            {
                Flip();
            }

            if (Input.GetButtonDown("Jump P" + playerID) && isGrounded) 
            {
                StartCoroutine(Jump());
            }

            if (Input.GetButtonDown("Dash P" + playerID) && dashAvailable)
            {
                CalculateDash();
            }
        }
    }

    private void FixedUpdate()
    {
        if (hasInput == true)
        {
            if (horizontalMove!=0) { playerAnimator.SetBool("IsMoving",true); }
            else { playerAnimator.SetBool("IsMoving", false); }
            

            rb.velocity = new Vector2((horizontalMove * currentMoveSpeed), rb.velocity.y);
        }
    }

    public IEnumerator Jump() 
    {
        yield return new WaitForSeconds(inputLag);
        playerSound.clip = soundManager.currentSounds[2];
        playerSound.Play();
        playerAnimator.SetTrigger("Jumping");
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

    public void CalculateDash()
    {
        GetInputRaw();

        Vector2 dashDir;
        if (Mathf.Abs(Input.GetAxis(horizontalStr + playerID)) > 0 || Mathf.Abs(Input.GetAxis(verticalStr + playerID)) > 0)
        {
            if (Mathf.Abs(Input.GetAxis(horizontalStr + playerID)) > Mathf.Abs(Input.GetAxis(verticalStr + playerID)))
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
            if (isFacingRight == true)
            {
                dashDir = new Vector2(1, 0);
            }
            else 
            {
                dashDir = new Vector2(-1, 0);
            }
        }
        StartCoroutine(Dash(dashDir));
    }

    public void RandomDash()
    {
        int randomValue = Random.Range(0, 4);

        Vector2[] listOfDir = new Vector2[] {new Vector2(-1, 0), new Vector2(1, 0), new Vector2(0, -1), new Vector2(0,1) };

        Vector2 dashDir = listOfDir[randomValue];

        StartCoroutine(Dash(dashDir));
    }

    IEnumerator Dash(Vector2 dashDirection) 
    {
        yield return new WaitForSeconds(inputLag);

        playerSound.clip = soundManager.currentSounds[0];
        playerSound.Play();

        StartCoroutine(DashCooldown());

        playerAnimator.SetFloat("DirY", Input.GetAxis(verticalStr + playerID));
        hasInput = false;


        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        rb.AddForce(dashDirection * dashForce, ForceMode2D.Impulse);

        dashState.SetActive(true);
        if(dashDirection.x != 0) 
        {
            dashState.GetComponent<BoxCollider2D>().size = new Vector2(1.4f, 0.7f);
        }
        else 
        {
            dashState.GetComponent<BoxCollider2D>().size = new Vector2(0.7f, 1.4f);
        }
        normalState.GetComponent<BoxCollider2D>().enabled = false;

        playerAnimator.SetBool("Dashing", true);
        

        yield return new WaitForSeconds(dashDuration);

        playerAnimator.SetBool("Dashing", false);

        hasInput = true;
        rb.gravityScale = initialGravityScale;
        rb.velocity = Vector2.zero;

        normalState.GetComponent<BoxCollider2D>().enabled = true;
        dashState.SetActive(false);
    }

    IEnumerator DashCooldown ()
    {
        dashAvailable = false;

        yield return new WaitForSeconds(dashCooldownTime);

        dashAvailable = true;
    }

    public void CheckBunnyJump() 
    {
        if (hasInput == true)
        {
            if (Input.GetButton("Jump P" + playerID))
            {
                StartCoroutine(Jump());
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
        if (Input.GetAxis(horizontalStr + playerID) != 0)
        {
            if (Input.GetAxis(horizontalStr + playerID) > 0)
                rawInputs.x = 1;
            if (Input.GetAxis(horizontalStr + playerID) < 0)
                rawInputs.x = -1;
        }
        else
        {
            rawInputs.x = 0;
        }

        if (Input.GetAxis(verticalStr + playerID) != 0)
        {
            if (Input.GetAxis(verticalStr + playerID) > 0)
                rawInputs.y = 1;
            if (Input.GetAxis(verticalStr + playerID) < 0)
                rawInputs.y = -1;
        }
        else
        {
            rawInputs.y = 0;
        }

        return rawInputs;
    }

    void Flip() 
    {
        isFacingRight = !isFacingRight;
        spriteRend.flipX = !spriteRend.flipX;
    }

    public void RetrieveInputs(float delay) 
    { 
        if (delay != 0) 
        {
            StartCoroutine(DelayInputs(delay));
        }
        else 
        {
            hasInput = true;
        }
    }

    IEnumerator DelayInputs(float delay) 
    {
        yield return new WaitForSeconds(delay);
        hasInput = true;
        //playerAnimator.SetBool("Hit", false);
    }

    public void CatchBall() 
    {
        playerBall.SetActive(true);
        playerSound.clip = soundManager.currentSounds[4];
        playerSound.Play();
    }

    IEnumerator SetJoystick() 
    {
        yield return new WaitForEndOfFrame();
        playerID = playerJoystick.joystickIndex;
    }
}
