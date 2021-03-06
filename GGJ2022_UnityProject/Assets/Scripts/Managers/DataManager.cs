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

    [Header("Dash")]
    public float dashForce;
    public float dashDuration;
    public float dashCooldownTime;
    public float maxEjectForce;
    public float minEjectForce;
    public float slowMotionDuration;
    public float slowMotionStrenght;
    public float bigSlowMotionStrenght;
    public float shockZoomStrenght;

    [Header("Death")]
    public float delayToDepop;
    public float delayToRespawn;
    public float delayForInputs;
    public List<Transform> spawnPoints = new List<Transform>();
}
