using UnityEngine;
using System.Collections;

public class BearKillTrigger : MonoBehaviour
{
    //private float _killCount;

    void OnTriggerEnter2D(Collider2D col)
    {
        var prey = col.GetComponent<PlayableCharacter>();
        if (prey != null && !prey.isDead)
        {
            prey.Die();
            GetComponentInParent<Bear>().Score();
            //_killCount++;

            //if (_killCount == 2)
            //{
            //    GameManager.GetInstance().EndGame(GetComponentInParent<PlayableCharacter>());
            //}
        }
    }
}
