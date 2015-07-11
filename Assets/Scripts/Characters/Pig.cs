using UnityEngine;
using System.Collections;

public class Pig : PlayableCharacter
{
    public Poop PoopPrefab;

    protected override IEnumerator StartPowerUp()
    {
        Instantiate(PoopPrefab, transform.position, Quaternion.identity);
        yield break;
    }

    public void Pickup()
    {

    }
}
