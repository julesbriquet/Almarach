using UnityEngine;
using System.Collections;

public class Bear : PlayableCharacter
{
    public float sprintSpeed;
    [Header("Sounds")]
    public AudioClip sprintSound;

    protected override IEnumerator StartPowerUp()
    {
        float _prevSpeed = speed;
        speed = sprintSpeed;
        _audioSource.PlayOneShot(sprintSound);
        yield return new WaitForSeconds(powerUpDuration / 1000f);
        speed = _prevSpeed;
    }
}
