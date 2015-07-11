using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ControlsSelection : MonoBehaviour
{
    List<Player> players;

    // Use this for initialization
    void Start()
    {
        players = new List<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (players.Count == 3 && players.All(p => p.characterType.HasValue))
        {
            // proceed to scene
            Application.LoadLevel("test-scene");
        }

        foreach (var scheme in new ControlScheme[]{ 
            ControlScheme.Gamepad1, ControlScheme.Gamepad2, ControlScheme.Gamepad3,
            ControlScheme.Keyboard5123, ControlScheme.KeyboardOKLM, ControlScheme.KeyboardZQSD})
        {
            if (Controls.UsePowerUp(scheme))
            {
                // if a player has this character scheme, validate his character type :
                if (players.Any(p => p.controls == scheme))
                {

                }
                else
                {
                    // checkin a player if necessary
                    if (players.Count < 3)
                        CheckInPlayer(scheme);
                }
            }
            var leftRight = Controls.GetDirection(scheme).x;
            if (leftRight != 0)
            {
                // change selected character
            }
        }
    }

    void CheckInPlayer(ControlScheme scheme)
    {
        var newPlayer = new Player() { id = players.Count + 1, controls = scheme };
        players.Add(newPlayer);

        bool isPad = scheme.ToString().StartsWith("Gamepad");

        // show his key mapping :
        GameObject.Find("p" + newPlayer.id + "-mapping-" + (isPad ? "pad" : "key")).SetActive(true);
    }
}
