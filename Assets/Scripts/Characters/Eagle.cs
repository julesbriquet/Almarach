using UnityEngine;
using System.Collections;

public class Eagle : PlayableCharacter
{
    [Header("Sounds")]
    public AudioClip flySound;
    public AudioClip pickupSound;

    public float fadeOpacity = 0.5f;
    // Eagle can :
    // pickup stuff
    // cross walls when using ability


    protected override IEnumerator StartPowerUp()
    {
        var renderer = GetComponent<SpriteRenderer>();
        var currentColor = renderer.material.color;
        renderer.material.color = new Color(currentColor.r, currentColor.g, currentColor.b, 0.5f);
        gameObject.layer = 11; // crosses static walls
        _audioSource.PlayOneShot(flySound);
        yield return new WaitForSeconds(powerUpDuration / 1000f);
        gameObject.layer = 9; // dont cross static walls
        renderer.material.color = new Color(currentColor.r, currentColor.g, currentColor.b, 1f);
    }

    public void Pickup(Pickup item)
    {
        CarriedItems.Add(item);
        _audioSource.PlayOneShot(pickupSound);
    }
}
