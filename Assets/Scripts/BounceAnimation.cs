using UnityEngine;
using System.Collections;

public class BounceAnimation : MonoBehaviour
{
	public Vector3 offset;
	public float period;
	public float lerpRate;
	
	private Vector3 _highestPosition;
	private Vector3 _lowestPosition;
	private float _timer;
	
	void Start()
	{
		_lowestPosition = transform.position;
		_highestPosition = transform.position + offset;
		_timer = Random.Range(0, period / 3);
	}
	
	void Update()
	{
		_timer += Time.deltaTime;
		if (_timer > period) {
			_timer -= period;
		} else if (_timer > period / 2) {
			transform.position = Vector3.Lerp(transform.position, _lowestPosition, lerpRate * Time.deltaTime);
		} else {
			transform.position = Vector3.Lerp(transform.position, _highestPosition, lerpRate * Time.deltaTime);
		}
	}
}
