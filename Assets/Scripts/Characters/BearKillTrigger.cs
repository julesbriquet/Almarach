using UnityEngine;
using System.Collections;

public class BearKillTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        var prey = col.GetComponent<PlayableCharacter>();
        if (prey != null)
        {
            prey.Die();
        }
    }
}
