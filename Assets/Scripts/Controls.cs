using UnityEngine;
using System.Collections;

public enum ControlScheme
{
    KeyboardZQSD,
    KeyboardOKLM,
    Keyboard5123,
    Gamepad1,
    Gamepad2,
    Gamepad3
}

public class Controls
{

    public static Vector2 GetDirection(ControlScheme controlScheme)
    {
        Vector2 direction = Vector2.zero;

        switch (controlScheme)
        {
            case ControlScheme.KeyboardZQSD:
                if (Input.GetKey(KeyCode.Z))
                    direction.y = 1;
                else if (Input.GetKey(KeyCode.S))
                    direction.y = -1;
                if (Input.GetKey(KeyCode.Q))
                    direction.x = -1;
                else if (Input.GetKey(KeyCode.D))
                    direction.x = 1;
                break;
            case ControlScheme.KeyboardOKLM:
                if (Input.GetKey(KeyCode.O))
                    direction.y = 1;
                else if (Input.GetKey(KeyCode.L))
                    direction.y = -1;
                if (Input.GetKey(KeyCode.K))
                    direction.x = -1;
                else if (Input.GetKey(KeyCode.M))
                    direction.x = 1;
                break;
            case ControlScheme.Keyboard5123:
                if (Input.GetKey(KeyCode.Keypad5))
                    direction.y = 1;
                else if (Input.GetKey(KeyCode.Keypad2))
                    direction.y = -1;
                if (Input.GetKey(KeyCode.Keypad1))
                    direction.x = -1;
                else if (Input.GetKey(KeyCode.Keypad3))
                    direction.x = 1;
                break;
            case ControlScheme.Gamepad1:
                direction = GetJoystickAxis(1);
                break;
            case ControlScheme.Gamepad2:
                direction = GetJoystickAxis(2);
                break;
            case ControlScheme.Gamepad3:
                direction = GetJoystickAxis(3);
                break;
            default:
                break;
        }

        return direction;
    }

    public static bool UsePowerUp(ControlScheme controlScheme)
    {
        switch (controlScheme)
        {
            case ControlScheme.KeyboardZQSD:
                return Input.GetKey(KeyCode.Tab);
            case ControlScheme.KeyboardOKLM:
                return Input.GetKey(KeyCode.Return);
            case ControlScheme.Keyboard5123:
                return Input.GetKey(KeyCode.Keypad0);
            case ControlScheme.Gamepad1:
                return GetJoystickButton(1);
            case ControlScheme.Gamepad2:
                return GetJoystickButton(2);
            case ControlScheme.Gamepad3:
                return GetJoystickButton(3);
        }
        return false;
    }

    private static Vector2 GetJoystickAxis(int joystickId)
    {
        return new Vector2(
            Input.GetAxis("joystick " + joystickId + " horizontal"), 
            Input.GetAxis("joystick " + joystickId + " vertical"));
    }

    private static bool GetJoystickButton(int joystickId)
    {
        return Input.GetKey("joystick " + joystickId + " button 0");
    }

    //public static Vector2 GetDirection(int playerId)
    //{
    //    Vector2 direction = Vector2.zero;

    //    if (playerId == 0)
    //    {
    //        if (Input.GetKey(KeyCode.Keypad5))
    //            direction.y = 1;
    //        else if (Input.GetKey(KeyCode.Keypad2))
    //            direction.y = -1;
    //        if (Input.GetKey(KeyCode.Keypad1))
    //            direction.x = -1;
    //        else if (Input.GetKey(KeyCode.Keypad3))
    //            direction.x = 1;
    //    }
    //    else if (playerId == 1)
    //    {
    //        if (Input.GetKey(KeyCode.Z))
    //            direction.y = 1;
    //        else if (Input.GetKey(KeyCode.S))
    //            direction.y = -1;
    //        if (Input.GetKey(KeyCode.Q))
    //            direction.x = -1;
    //        else if (Input.GetKey(KeyCode.D))
    //            direction.x = 1;
    //    }
    //    else if (playerId == 2)
    //    {
    //        if (Input.GetKey(KeyCode.O))
    //            direction.y = 1;
    //        else if (Input.GetKey(KeyCode.L))
    //            direction.y = -1;
    //        if (Input.GetKey(KeyCode.K))
    //            direction.x = -1;
    //        else if (Input.GetKey(KeyCode.M))
    //            direction.x = 1;
    //    }

    //    return direction;
    //}

    //public static bool UsePowerUp(int playerId)
    //{
    //    if (playerId == 0)
    //    {
    //        if (Input.GetKey(KeyCode.Keypad0))
    //            return true;
    //    }
    //    else if (playerId == 1)
    //    {
    //        if (Input.GetKey(KeyCode.Tab))
    //            return true;
    //    }
    //    else if (playerId == 2)
    //    {
    //        if (Input.GetKey(KeyCode.Return))
    //            return true;
    //    }
    //    return false;
    //}
}
