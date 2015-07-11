using UnityEngine;
using System.Collections;

public class Controls
{
    public static Vector2 GetDirection(int playerId)
    {
        Vector2 direction = Vector2.zero;

        if (playerId == 0)
        {
            if (Input.GetKey(KeyCode.Keypad5))
                direction.y = 1;
            else if (Input.GetKey(KeyCode.Keypad2))
                direction.y = -1;
            if (Input.GetKey(KeyCode.Keypad1))
                direction.x = -1;
            else if (Input.GetKey(KeyCode.Keypad3))
                direction.x = 1;
        }
        else if (playerId == 1)
        {
            if (Input.GetKey(KeyCode.Z))
                direction.y = 1;
            else if (Input.GetKey(KeyCode.S))
                direction.y = -1;
            if (Input.GetKey(KeyCode.Q))
                direction.x = -1;
            else if (Input.GetKey(KeyCode.D))
                direction.x = 1;
        }
        else if (playerId == 2)
        {
            if (Input.GetKey(KeyCode.O))
                direction.y = 1;
            else if (Input.GetKey(KeyCode.L))
                direction.y = -1;
            if (Input.GetKey(KeyCode.K))
                direction.x = -1;
            else if (Input.GetKey(KeyCode.M))
                direction.x = 1;
        }

        return direction;
    }

    public static bool UsePowerUp(int playerId)
    {
        if (playerId == 0)
        {
            if (Input.GetKey(KeyCode.Keypad0))
                return true;
        }
        else if (playerId == 1)
        {
            if (Input.GetKey(KeyCode.Tab))
                return true;
        }
        else if (playerId == 2)
        {
            if (Input.GetKey(KeyCode.Return))
                return true;
        }
        return false;
    }
}
