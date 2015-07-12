using UnityEngine;
using System.Collections;

public enum CollectibleType
{
    Eagle,
    Pig
}

public class Pickup : MonoBehaviour
{
    public CollectibleType CollectibleType;
    public bool isPickedUp = false;
    public ParticleSystem Effect;
    private ParticleSystem _effect;

    void Start()
    {
        _effect = (ParticleSystem)Instantiate(Effect, transform.position, Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        switch (CollectibleType)
        {
            case CollectibleType.Eagle:
                var eagle = col.gameObject.GetComponent<Eagle>();
                if (eagle != null)
                {
                    eagle.Pickup(this);
                    PickedUp();
                }
                break;
            case CollectibleType.Pig:
                var pig = col.gameObject.GetComponent<Pig>();
                if (pig != null)
                {
                    pig.Pickup(this);
                    PickedUp();
                }
                break;
        }
    }

    public void Respawn()
    {
        _effect.Play();
        isPickedUp = false;
        gameObject.SetActive(true);
    }

    private void PickedUp()
    {
        gameObject.SetActive(false);
        isPickedUp = true;
    }
}
