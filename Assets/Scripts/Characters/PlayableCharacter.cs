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

	public float remainingTime;

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

	public AudioClip respawnSound;

    // Use this for initialization
    Vector2 _startPos;
    protected virtual void Start()
    {
		remainingTime = powerUpCooldown;
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
		remainingTime += Time.deltaTime * 1000;


		if (remainingTime >= powerUpCooldown) {
			remainingTime = powerUpCooldown;
		}

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
		remainingTime = 0.0f;
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
		StartCoroutine(DoBlinks(0.1f));
        _audioSource.PlayOneShot(stunSound);
        yield return new WaitForSeconds(duration / 1000f);
        isStunned = false;
		//StopCoroutine (DoBlinks(0);
        Debug.Log(gameObject.name + " recovered.");
    }

	IEnumerator DoBlinks(float blinkTime) {
		while (isStunned) {
			GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;
			yield return new WaitForSeconds(blinkTime);
		}
		GetComponent<Renderer>().enabled = true;
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
		
		_audioSource.PlayOneShot (respawnSound);
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
