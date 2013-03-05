using UnityEngine;
using System.Collections;

public class InputManager
{
    public static bool GetKeyDownUp()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)
            || Input.GetKeyDown(KeyCode.Joystick1Button3)
            || Input.GetKeyDown(KeyCode.JoystickButton19))
        {
            return true;
        }
        return false;
    }

    public static bool GetKeyDownLeft()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)
            || Input.GetKeyDown(KeyCode.Joystick1Button2)
            || Input.GetKeyDown(KeyCode.JoystickButton18))
        {
            return true;
        }
        return false;
    }

    public static bool GetKeyDownDown()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow)
            || Input.GetKeyDown(KeyCode.Joystick1Button0)
            || Input.GetKeyDown(KeyCode.Joystick1Button6)
            || Input.GetKeyDown(KeyCode.JoystickButton16))
        {
            return true;
        }
        return false;
    }

    public static bool GetKeyDownRight()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow)
            || Input.GetKeyDown(KeyCode.Joystick1Button1)
            || Input.GetKeyDown(KeyCode.JoystickButton17))
        {
            return true;
        }
        return false;
    }
    public static bool GetKeyDownAction()
    {
        if (Input.GetButtonDown("Fire1")
            || Input.GetKeyDown(KeyCode.KeypadEnter)
            || Input.GetKeyDown(KeyCode.Joystick1Button7)
            || Input.GetKeyDown(KeyCode.Joystick1Button0)
            || Input.GetKeyDown(KeyCode.JoystickButton9)
            || Input.GetKeyDown(KeyCode.JoystickButton16))
        {
            return true;
        }
        return false;
    }
}
