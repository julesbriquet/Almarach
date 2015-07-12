using UnityEngine;
using System.Collections;

public class BearKillTrigger : MonoBehaviour
{
    public AudioClip killSound;
    //private float _killCount;
    private AudioSource _audioSource;

    void Start()
    {
      _audioSource = GetComponentInParent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        var prey = col.GetComponent<PlayableCharacter>();
        if (prey != null && !prey.isDead)
        {
            prey.Die();
            GetComponentInParent<Bear>().Score();
            _audioSource.PlayOneShot(killSound);
            //_killCount++;

            //if (_killCount == 2)
            //{
            //    GameManager.GetInstance().EndGame(GetComponentInParent<PlayableCharacter>());
            //}
        }
    }
}
