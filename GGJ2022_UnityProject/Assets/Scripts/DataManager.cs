using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [Header("Rigidbody")]
    public float mass;
    public float linearDrag;
    public float gravityScale;

    [Header("Player Controls")]
    public float moveSpeed;
    public float jumpForce;
    public float airControl;
    public float dashForce;
    public float dashDuration;
    public float dashCooldownTime;
}
