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

    void OnTriggerEnter2D(Collider2D col)
    {
        switch (CollectibleType)
        {
            case CollectibleType.Eagle:
                var eagle = col.gameObject.GetComponent<Eagle>();
                if (eagle != null)
                {
                    eagle.Pickup();
                    PickedUp();
                }
                break;
            case CollectibleType.Pig:
                var pig = col.gameObject.GetComponent<Pig>();
                if (pig != null)
                {
                    pig.Pickup();
                    PickedUp();
                }
                break;
        }
    }

    private void PickedUp()
    {
        gameObject.SetActive(false);
        isPickedUp = true;
    }
}
