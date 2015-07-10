using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Rigidbody2D))]
public abstract class PlayableCharacter : MonoBehaviour
{
    Rigidbody2D _rb;

    [Tooltip("TODO TEMPORARY. ID FOR THE CONTROLS SCHEME")]
    public int playerId;

    public float speed;
    public float inertiaFriction;

    public bool powerUpAvailable = true;
    [Tooltip("Duration of powerup in milliseconds")]
    public float powerUpDuration;
    [Tooltip("Time for powerup to reload in milliseconds")]
    public float powerUpCooldown;

    // Use this for initialization
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 direction = Controls.GetDirection(playerId);
        if (direction != Vector2.zero)
        {
            _rb.velocity = Vector2.MoveTowards(_rb.velocity, direction * speed, 1);
        }
        else
        {
            _rb.velocity = Vector2.MoveTowards(_rb.velocity, Vector2.zero, inertiaFriction);
        }

        if (Controls.UsePowerUp(playerId) && powerUpAvailable)
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

    public void Die()
    {
        gameObject.SetActive(false);
    }
}
