using UnityEngine;
using System.Collections;

public class Bear : PlayableCharacter
{
    public float sprintSpeed;

    protected override IEnumerator StartPowerUp()
    {
        float _prevSpeed = speed;
        speed = sprintSpeed;
        yield return new WaitForSeconds(powerUpDuration / 1000f);
        speed = _prevSpeed;
    }
}
