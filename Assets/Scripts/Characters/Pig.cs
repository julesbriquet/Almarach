using UnityEngine;
using System.Collections;

public class Pig : PlayableCharacter
{
    public Poop PoopPrefab;
    [Header("Sounds")]
    public AudioClip poopSound;
    public AudioClip pickupSound;

    private float _initialSpeed;

    protected override void Start()
    {
        base.Start();
        _initialSpeed = speed;
    }

    protected override IEnumerator StartPowerUp()
    {
        Instantiate(PoopPrefab, transform.position, Quaternion.identity);
        _audioSource.PlayOneShot(poopSound);
        yield break;
    }

    public override void Die()
    {
        base.Die();
        speed = _initialSpeed;
    }

    protected override void ReplaceAllItems()
    {
      base.ReplaceAllItems();
      speed = _initialSpeed;
    }

    public void Pickup(Pickup item)
    {
        CarriedItems.Add(item);
        speed *= 0.8f;
        _audioSource.PlayOneShot(pickupSound);
    }
}
