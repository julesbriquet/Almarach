using UnityEngine;
using System.Collections;

public class Controls
{
    public static Vector2 GetDirection(int playerId)
    {
        Vector2 direction = Vector2.zero;

        if (playerId == 0)
        {
            if (Input.GetKey(KeyCode.UpArrow))
                direction.y = 1;
            else if (Input.GetKey(KeyCode.DownArrow))
                direction.y = -1;
            if (Input.GetKey(KeyCode.LeftArrow))
                direction.x = -1;
            else if (Input.GetKey(KeyCode.RightArrow))
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
            if (Input.GetKey(KeyCode.Space))
                return true;
        }
        return false;
    }
}
