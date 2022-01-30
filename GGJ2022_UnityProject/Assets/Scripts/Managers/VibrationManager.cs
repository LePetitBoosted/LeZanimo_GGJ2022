using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class VibrationManager : MonoBehaviour
{
    public HellVibrationsMalus hellVibrations;

    public void SetVibration(float intensity, float duration, PlayerNumber playerNumber)
    {

        Mathf.Clamp01(intensity);
        if (!isPlayerInfluencedByHellVibrations(playerNumber))
        {

            if (playerNumber == PlayerNumber.PlayerOne)
            {
                GamePad.SetVibration(PlayerIndex.One, intensity, intensity);
            }
            else if (playerNumber == PlayerNumber.PlayerTwo)
            {
                GamePad.SetVibration(PlayerIndex.Two, intensity, intensity);
            }
            else
            {
                Debug.Log("Player Number not valid");
            }

            if (duration != -1)
            {
                StartCoroutine(StopVibrationAfterDelay(duration, playerNumber));
            }
        }
    }

    public void ActiveHellVibrations(PlayerNumber playerNumber)
    {
        if (playerNumber == PlayerNumber.PlayerOne)
        {
            GamePad.SetVibration(PlayerIndex.One, 1f, 1f);
        }
        else if (playerNumber == PlayerNumber.PlayerTwo)
        {
            GamePad.SetVibration(PlayerIndex.Two, 1f, 1f);
        }
        else
        {
            Debug.Log("Player Number not valid");
        }
    }

    IEnumerator StopVibrationAfterDelay(float delay, PlayerNumber playerNum)
    {
        yield return new WaitForSeconds(delay);

        if (!isPlayerInfluencedByHellVibrations(playerNum))
        {
            if (playerNum == PlayerNumber.PlayerOne)
            {
                GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
            }
            else if (playerNum == PlayerNumber.PlayerTwo)
            {
                GamePad.SetVibration(PlayerIndex.Two, 0f, 0f);
            }
        }
    }

    public bool isPlayerInfluencedByHellVibrations(PlayerNumber playerNumber)
    {        
        bool output = false;
        
        if (hellVibrations.gameObject.activeSelf == true)
        {
            if (hellVibrations.target.GetComponent<PlayerControls>().playerNumber == playerNumber)
            {
                output = true;
            }
        }

        return output;
        
    }
}
