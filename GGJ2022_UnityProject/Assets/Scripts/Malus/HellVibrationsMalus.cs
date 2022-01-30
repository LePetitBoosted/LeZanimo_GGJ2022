using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class HellVibrationsMalus : MonoBehaviour
{
    public GameObject target;
    public VibrationManager vibrationManager;


    private void OnEnable()
    {
        //vibrationManager.SetVibration(1f, -1f, target.GetComponent<PlayerControls>().playerNumber);

        StartCoroutine(WaitForTargetAndVibrate());
    }

    private void OnDisable()
    {
        vibrationManager.SetVibration(0f, -1f, target);

        /*vibrationManager.SetVibration(0f, -1f, PlayerNumber.PlayerOne);
        vibrationManager.SetVibration(0f, -1f, PlayerNumber.PlayerOne);*/
    }

    IEnumerator WaitForTargetAndVibrate()
    {
        yield return new WaitForEndOfFrame();

        target = GetComponentInParent<MalusManager>().targetPlayer;

        vibrationManager.ActiveHellVibrations(target);
    }

}
