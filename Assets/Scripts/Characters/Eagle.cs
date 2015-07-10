using UnityEngine;
using System.Collections;

public class Eagle : PlayableCharacter
{
    // Eagle can :
    // pickup stuff
    // cross walls when using ability


    // Update is called once per frame
    void Update()
    {

    }

    protected override IEnumerator StartPowerUp()
    {
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(powerUpDuration / 1000f);
        GetComponent<Collider2D>().enabled = true;
    }

    public void Pickup()
    {

    }
}
