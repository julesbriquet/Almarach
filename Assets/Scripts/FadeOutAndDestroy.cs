using UnityEngine;
using System.Collections;

public class FadeOutAndDestroy : MonoBehaviour
{
    [Tooltip("delay before object is destroyed in milliseconds")]
    public float destroyDelay;

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, destroyDelay / 1000f);
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        var renderer = GetComponent<Renderer>();
        var baseColor = renderer.material.color;

        
        while (true) // until object destroyed by Start method and delay
        {
            baseColor.a -= 1f / (destroyDelay / 10);
            renderer.material.color = baseColor;
            yield return new WaitForSeconds(10 / 1000f); // every 10ms
        }
    }
}
