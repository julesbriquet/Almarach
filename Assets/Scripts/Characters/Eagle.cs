using UnityEngine;
using System.Collections;

public class Eagle : PlayableCharacter
{
    // Eagle can :
    // pickup stuff
    // cross walls when using ability


    protected override IEnumerator StartPowerUp()
    {
        gameObject.layer = 11; // crosses static walls
        yield return new WaitForSeconds(powerUpDuration / 1000f);
        gameObject.layer = 9; // dont cross static walls
    }

    public void Pickup(Pickup item)
    {
        CarriedItems.Add(item);
    }
}
