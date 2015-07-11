using UnityEngine;
using System.Collections;

public class BearKillTrigger : MonoBehaviour
{
    private float _killCount;

    void OnTriggerEnter2D(Collider2D col)
    {
        var prey = col.GetComponent<PlayableCharacter>();
        if (prey != null)
        {
            prey.Die();
            _killCount++;

            if (_killCount == 2)
            {
                Debug.Log("bear has won.");
            }
        }
    }
}
