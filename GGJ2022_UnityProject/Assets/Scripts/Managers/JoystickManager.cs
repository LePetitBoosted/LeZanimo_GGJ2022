using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class JoystickManager : MonoBehaviour
{
    public List<Joystick> joysticks = new List<Joystick>();

    public List<PlayerIndex> connectedPlayerIndex = new List<PlayerIndex>();

    [SerializeField] GameObject playerOne;
    [SerializeField] GameObject playerTwo;

    private void Awake()
    {
        int joystickNameIndex = 0;
        foreach(string joystickName in Input.GetJoystickNames()) 
        {
            joystickNameIndex++;

            if (joystickName != "" && joystickNameIndex < 6) 
            {
                Joystick tempJoystick = new Joystick();
                tempJoystick.joystickName = joystickName;
                tempJoystick.joystickIndex = joystickNameIndex;

                joysticks.Add(tempJoystick);
            }
        }

        playerOne.GetComponent<PlayerControls>().playerJoystick = joysticks[0];
        playerTwo.GetComponent<PlayerControls>().playerJoystick = joysticks[1];

        FindXInputIndex();
    }

    void FindXInputIndex() 
    {
        int index = 1;

        while (index < 5)
        {
            PlayerIndex currentPlayerIndex = PlayerIndex.One;

            switch (index)
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

            if (GamePad.GetState(currentPlayerIndex).IsConnected == true) 
            {
                connectedPlayerIndex.Add(currentPlayerIndex);
            }

            index++;
        }
    }
}

public class Joystick 
{
    public string joystickName;
    public int joystickIndex;
}
