using UnityEngine;
using System.Collections;

public class Poop : MonoBehaviour
{
    [Tooltip("Stun duration in milliseconds")]
    public float stunDuration;

    [Tooltip("delay until the poop disappears in milliseconds")]
    public float duration;

    public Sprite[] Frames;
    public float FrameDuration;

    // Use this for initialization
    void Start()
    {
        Destroy(this.gameObject, duration / 1000f);
        StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        var renderer = GetComponent<SpriteRenderer>();
        int nextFrame = 0;
        while (true)
        {
            renderer.sprite = Frames[++nextFrame % Frames.Length];
            yield return new WaitForSeconds(FrameDuration / 1000f);
        }
    }

    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.GetComponent<Eagle>() != null || player.GetComponent<Bear>() != null)
        {
            // stun either 
            player.GetComponent<PlayableCharacter>().Stun(stunDuration);
            Destroy(gameObject);
        }
    }

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, GetComponent<CircleCollider2D>().radius * 0.8f);
    //}
}
