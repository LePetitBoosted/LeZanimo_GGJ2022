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
            /*PlayerIndex currentPlayerIndex = PlayerIndex.One;

            switch (targetPlayer.GetComponent<PlayerControls>().playerJoystick.joystickIndex) 
            {
                case 1:
                    currentPlayerIndex = PlayerIndex.One;
                    break;

                case 2:
                    currentPlayerIndex = PlayerIndex.Two;
                    break;

                case 3:
                    currentPlayerIndex = PlayerIndex.Three;
                    break;

                case 4:
                    currentPlayerIndex = PlayerIndex.Four;
                    break;

                default:
                    Debug.Log("Not valid joystick index");
                    break;
            }

            GamePad.SetVibration(currentPlayerIndex, intensity, intensity);*/

            if (targetPlayer.GetComponent<PlayerControls>().playerNumber == PlayerNumber.PlayerOne)
            {
                GamePad.SetVibration(joystickManager.connectedPlayerIndex[0], intensity, intensity);
            }
            else if (targetPlayer.GetComponent<PlayerControls>().playerNumber == PlayerNumber.PlayerTwo)
            {
                GamePad.SetVibration(joystickManager.connectedPlayerIndex[1], intensity, intensity);
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
        /*PlayerIndex currentPlayerIndex = PlayerIndex.One;

        switch (targetPlayer.GetComponent<PlayerControls>().playerJoystick.joystickIndex)
        {
            case 1:
                currentPlayerIndex = PlayerIndex.One;
                break;

            case 2:
                currentPlayerIndex = PlayerIndex.Two;
                break;

            case 3:
                currentPlayerIndex = PlayerIndex.Three;
                break;

            case 4:
                currentPlayerIndex = PlayerIndex.Four;
                break;

            default:
                Debug.Log("Not valid joystick index");
                break;
        }

        GamePad.SetVibration(currentPlayerIndex, 1f, 1f);*/

        if (targetPlayer.GetComponent<PlayerControls>().playerNumber == PlayerNumber.PlayerOne)
        {
            GamePad.SetVibration(joystickManager.connectedPlayerIndex[0], 1f, 1f);
        }
        else if (targetPlayer.GetComponent<PlayerControls>().playerNumber == PlayerNumber.PlayerTwo)
        {
            GamePad.SetVibration(joystickManager.connectedPlayerIndex[1], 1f, 1f);
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
            /*PlayerIndex currentPlayerIndex = PlayerIndex.One;

            switch (targetPlayer.GetComponent<PlayerControls>().playerJoystick.joystickIndex)
            {
                case 1:
                    currentPlayerIndex = PlayerIndex.One;
                    break;

                case 2:
                    currentPlayerIndex = PlayerIndex.Two;
                    break;

                case 3:
                    currentPlayerIndex = PlayerIndex.Three;
                    break;

                case 4:
                    currentPlayerIndex = PlayerIndex.Four;
                    break;

                default:
                    Debug.Log("Not valid joystick index");
                    break;
            }

            GamePad.SetVibration(currentPlayerIndex, 0f, 0f);*/

            if (targetPlayer.GetComponent<PlayerControls>().playerNumber == PlayerNumber.PlayerOne)
            {
                GamePad.SetVibration(joystickManager.connectedPlayerIndex[0], 0f, 0f);
            }
            else if (targetPlayer.GetComponent<PlayerControls>().playerNumber == PlayerNumber.PlayerTwo)
            {
                GamePad.SetVibration(joystickManager.connectedPlayerIndex[1], 0f, 0f);
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
