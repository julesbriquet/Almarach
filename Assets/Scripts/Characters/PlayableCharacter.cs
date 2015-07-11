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

    // Use this for initialization
    Vector2 _startPos;
    protected virtual void Start()
    {
        _startPos = transform.position;
        CarriedItems = new List<Pickup>();
        _rb = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (!isStunned)
        //{
            Vector2 direction = Controls.GetDirection(controls);
            if (!isDead && !isStunned && direction != Vector2.zero)
            {
                _rb.velocity = Vector2.MoveTowards(_rb.velocity, direction.normalized * speed, 1);
            }
            else
            {
                _rb.velocity = Vector2.MoveTowards(_rb.velocity, Vector2.zero, inertiaFriction);
            }

            if (!isDead && !isStunned && Controls.UsePowerUp(controls) && powerUpAvailable)
                StartCoroutine(UsePowerUp());
        //}
        //else
        //{

        //}
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

        yield return new WaitForSeconds(RespawnTime / 1000f);
        ReplaceAllItems();
        
        transform.position = _startPos;

        isDead = false;
    }

    protected virtual void ReplaceAllItems()
    {
        foreach (var item in CarriedItems)
        {
            item.gameObject.SetActive(true);
        }
        CarriedItems.Clear();
    }
}
