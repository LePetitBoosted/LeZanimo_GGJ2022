using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermanentJumpMalus : MonoBehaviour
{
    [SerializeField] MalusManager malusManager;

    private void OnEnable()
    {
        Debug.LogWarning("Malus");
    }

    private void Update()
    {
        if (malusManager.targetPlayer.GetComponent<PlayerControls>().isGrounded)
        {
            StartCoroutine(malusManager.targetPlayer.GetComponent<PlayerControls>().Jump());
            
        }
    }

    private void OnDisable()
    {
        Debug.LogWarning("Malus ended");
    }

}
