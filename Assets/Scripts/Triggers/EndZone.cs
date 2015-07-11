using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class EndZone : MonoBehaviour
{
    public PlayableCharacter CharacterThatTriggers;
    public Pickup[] CollectiblesToGet;

    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject != CharacterThatTriggers.gameObject)
            return;

        foreach (var col in CollectiblesToGet)
	    {
            if (col.gameObject.activeSelf) // collectibles remaining: do nothing
                return;
	    }
        // all collectibles are gathered: game is won.
        CharacterThatTriggers.Score();
        //GameManager.GetInstance().EndGame(CharacterThatTriggers);
    }
}
