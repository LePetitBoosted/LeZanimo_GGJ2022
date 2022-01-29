using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class HellVibrationsMalus : MonoBehaviour
{
    GameObject target;

    private void OnEnable()
    {
        StartCoroutine(SetVibrations());
    }

    private void OnDisable()
    {
        GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
        GamePad.SetVibration(PlayerIndex.Two, 0f, 0f);
    }

    IEnumerator SetVibrations()
    {
        yield return new WaitForEndOfFrame();
        target = transform.parent.GetComponent<MalusManager>().targetPlayer;
        if(target.GetComponent<PlayerControls>().playerNumber == PlayerNumber.PlayerOne) 
        {
            GamePad.SetVibration(PlayerIndex.Two, 1f, 1f);
        }
        else 
        {
            GamePad.SetVibration(PlayerIndex.One, 1f, 1f);
        }
    }
}
