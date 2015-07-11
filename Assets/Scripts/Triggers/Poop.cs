using UnityEngine;
using System.Collections;

public class Poop : MonoBehaviour
{
    [Tooltip("Stun duration in milliseconds")]
    public float stunDuration;

    [Tooltip("delay until the poop disappears in milliseconds")]
    public float duration;

    // Use this for initialization
    void Start()
    {
        Destroy(this.gameObject, duration / 1000f);
    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.GetComponent<Eagle>() != null || player.GetComponent<Bear>() != null)
        {
            // stun either 
            player.GetComponent<PlayableCharacter>().Stun(stunDuration);
            Destroy(gameObject);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, GetComponent<CircleCollider2D>().radius * 0.8f);
    }
}
