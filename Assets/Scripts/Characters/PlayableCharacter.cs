using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public enum CharacterType
{
    Eagle,
    Pig,
    Bear
}

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class PlayableCharacter : MonoBehaviour
{
    Rigidbody2D _rb;

    public bool isStunned;
    public bool isDead;

    public ControlScheme controls;

    public float speed;
    public float inertiaFriction;

    public bool powerUpAvailable = true;
    [Tooltip("Duration of powerup in milliseconds")]
    public float powerUpDuration;
    [Tooltip("Time for powerup to reload in milliseconds")]
    public float powerUpCooldown;
    [Tooltip("Respawn delay in milliseconds")]
    public float RespawnTime;

    public int _score;
    public GameObject scoreCount;

    public List<Pickup> CarriedItems;

    public AudioClip stunSound;

    protected AudioSource _audioSource;
    protected Animator _animator;

    public ParticleSystem RespawnEffect;
    private ParticleSystem _respawnEffect;

    // Use this for initialization
    Vector2 _startPos;
    protected virtual void Start()
    {
        _startPos = transform.position;
        CarriedItems = new List<Pickup>();
        _rb = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        _respawnEffect = (ParticleSystem)Instantiate(RespawnEffect, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 direction = Controls.GetDirection(controls);
        if (!isDead && !isStunned && direction != Vector2.zero)
        {
            _rb.velocity = Vector2.MoveTowards(_rb.velocity, direction.normalized * speed, 1);

            // face correct direction :
            if (_rb.velocity.x != 0)
                transform.localScale = new Vector3(_rb.velocity.x > 0 ? -1 : 1, 1, 1);
            else if ((_rb.velocity.y != 0))
                transform.localScale = new Vector3(_rb.velocity.y > 0 ? -1 : 1, 1, 1);

            // walk :
            _animator.SetBool("IsWalking", true);
        }
        else
        {
            _rb.velocity = Vector2.MoveTowards(_rb.velocity, Vector2.zero, inertiaFriction);
            _animator.SetBool("IsWalking", false);
        }

        if (!isDead && !isStunned && Controls.UsePowerUp(controls) && powerUpAvailable)
            StartCoroutine(UsePowerUp());
    }

    protected virtual IEnumerator StartPowerUp()
    {
        yield return null;
    }

    private IEnumerator UsePowerUp()
    {
        Debug.Log(gameObject.name + " used powerup.");
        powerUpAvailable = false;
        StartCoroutine(StartPowerUp());
        yield return new WaitForSeconds(powerUpCooldown / 1000f);
        powerUpAvailable = true;
        Debug.Log(gameObject.name + " powerup available again");
    }

    public void Stun(float duration)
    {
        StartCoroutine(DoStun(duration));
    }

    private IEnumerator DoStun(float duration)
    {
        Debug.Log(gameObject.name + " stunned !");
        isStunned = true;
        _rb.velocity = Vector2.zero;
        _audioSource.PlayOneShot(stunSound);
        yield return new WaitForSeconds(duration / 1000f);
        isStunned = false;
        Debug.Log(gameObject.name + " recovered.");
    }

    public void Score()
    {
        _score++;
        scoreCount.GetComponent<Text>().text = _score.ToString();
        ReplaceAllItems();
    }

    public virtual void Die()
    {
        if (!isDead)
            StartCoroutine(Respawn());
    }

    protected virtual IEnumerator Respawn()
    {
        isDead = true;
        GetComponent<Renderer>().enabled = false;

        _respawnEffect.Play();

        yield return new WaitForSeconds(RespawnTime / 1000f);
        ReplaceAllItems();
        
        transform.position = _startPos;
        GetComponent<Renderer>().enabled = true;
        //_respawnEffect.Play();

        isDead = false;
    }

    protected virtual void ReplaceAllItems()
    {
        foreach (var item in CarriedItems)
        {
            item.Respawn();
        }
        CarriedItems.Clear();
    }
}
