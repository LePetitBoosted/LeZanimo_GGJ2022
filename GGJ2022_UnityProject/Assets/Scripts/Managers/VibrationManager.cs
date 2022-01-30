using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class VibrationManager : MonoBehaviour
{
    JoystickManager joystickManager;

    public bool invertedControllerRumble;
    public HellVibrationsMalus hellVibrations;

    private void Awake()
    {
        joystickManager = FindObjectOfType<JoystickManager>();
    }

    public void SetVibration(float intensity, float duration, GameObject targetPlayer)
    {
        Mathf.Clamp01(intensity);
        if (!isPlayerInfluencedByHellVibrations(targetPlayer))
        {
            if (targetPlayer.GetComponent<PlayerControls>().playerNumber == PlayerNumber.PlayerOne)
            {
                if (invertedControllerRumble == false)
                {
                    GamePad.SetVibration(joystickManager.connectedPlayerIndex[0], intensity, intensity);
                }
                else 
                {
                    GamePad.SetVibration(joystickManager.connectedPlayerIndex[1], intensity, intensity);
                }
            }
            else if (targetPlayer.GetComponent<PlayerControls>().playerNumber == PlayerNumber.PlayerTwo)
            {
                if (invertedControllerRumble == false)
                {
                    GamePad.SetVibration(joystickManager.connectedPlayerIndex[1], intensity, intensity);
                }
                else
                {
                    GamePad.SetVibration(joystickManager.connectedPlayerIndex[0], intensity, intensity);
                }
            }
            else
            {
                Debug.Log("Player Number not valid");
            }

            if (duration != -1)
            {
                StartCoroutine(StopVibrationAfterDelay(duration, targetPlayer));
            }
        }
    }

    public void ActiveHellVibrations(GameObject targetPlayer)
    {
        if (targetPlayer.GetComponent<PlayerControls>().playerNumber == PlayerNumber.PlayerOne)
        {
            if (invertedControllerRumble == false)
            {
                GamePad.SetVibration(joystickManager.connectedPlayerIndex[0], 1f, 1f);
            }
            else
            {
                GamePad.SetVibration(joystickManager.connectedPlayerIndex[1], 1f, 1f);
            }
        }
        else if (targetPlayer.GetComponent<PlayerControls>().playerNumber == PlayerNumber.PlayerTwo)
        {
            if (invertedControllerRumble == false)
            {
                GamePad.SetVibration(joystickManager.connectedPlayerIndex[1], 1f, 1f);
            }
            else
            {
                GamePad.SetVibration(joystickManager.connectedPlayerIndex[0], 1f, 1f);
            }
        }
        else
        {
            Debug.Log("Player Number not valid");
        }
    }

    IEnumerator StopVibrationAfterDelay(float delay, GameObject targetPlayer)
    {
        yield return new WaitForSeconds(delay);

        if (!isPlayerInfluencedByHellVibrations(targetPlayer))
        {
            if (targetPlayer.GetComponent<PlayerControls>().playerNumber == PlayerNumber.PlayerOne)
            {
                if (invertedControllerRumble == false)
                {
                    GamePad.SetVibration(joystickManager.connectedPlayerIndex[0], 0f, 0f);
                }
                else
                {
                    GamePad.SetVibration(joystickManager.connectedPlayerIndex[1], 0f, 0f);
                }
            }
            else if (targetPlayer.GetComponent<PlayerControls>().playerNumber == PlayerNumber.PlayerTwo)
            {
                if (invertedControllerRumble == false)
                {
                    GamePad.SetVibration(joystickManager.connectedPlayerIndex[1], 0f, 0f);
                }
                else
                {
                    GamePad.SetVibration(joystickManager.connectedPlayerIndex[0], 0f, 0f);
                }
            }
        }
    }



    public bool isPlayerInfluencedByHellVibrations(GameObject playerTarget)
    {        
        bool output = false;
        
        if (hellVibrations.gameObject.activeSelf == true)
        {
            if (hellVibrations.target == playerTarget)
            {
                output = true;
            }
        }

        return output;
        
    }
}
