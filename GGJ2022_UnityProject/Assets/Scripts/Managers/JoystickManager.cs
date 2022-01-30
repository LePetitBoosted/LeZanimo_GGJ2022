using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickManager : MonoBehaviour
{
    public List<Joystick> joysticks = new List<Joystick>();

    [SerializeField] GameObject playerOne;
    [SerializeField] GameObject playerTwo;

    private void Awake()
    {
        int joystickNameIndex = 0;
        foreach(string joystickName in Input.GetJoystickNames()) 
        {
            if(joystickName != "") 
            {
                joystickNameIndex++;

                Joystick tempJoystick = new Joystick();
                tempJoystick.joystickName = joystickName;
                tempJoystick.joystickIndex = joystickNameIndex;

                joysticks.Add(tempJoystick);
            }
        }

        playerOne.GetComponent<PlayerControls>().playerJoystick = joysticks[0];
        playerTwo.GetComponent<PlayerControls>().playerJoystick = joysticks[1];
    }
}

public class Joystick 
{
    public string joystickName;
    public int joystickIndex;
}
