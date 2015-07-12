using UnityEngine;
using System.Collections;

public class Bear : PlayableCharacter
{
    public float sprintSpeed;
    public AudioClip sprintSound;

    public Transform TrailSprite;
    public float TrailFrequency;

    private bool _isSprinting;

    protected override IEnumerator StartPowerUp()
    {
        
        float _prevSpeed = speed;
        speed = sprintSpeed;
        _audioSource.PlayOneShot(sprintSound);
        _isSprinting = true;
        StartCoroutine(LeaveTrails());
        yield return new WaitForSeconds(powerUpDuration / 1000f);
        _isSprinting = false;
        speed = _prevSpeed;
    }

    protected IEnumerator LeaveTrails()
    {
        while (_isSprinting)
        { 
            // Create trail sprites
            var sprite = (Transform)Instantiate(TrailSprite, transform.position, Quaternion.identity);
            sprite.localScale = new Vector3(transform.localScale.x, 1, 1);
            yield return new WaitForSeconds(TrailFrequency / 1000f);
        }
    }
}
