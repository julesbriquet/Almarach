using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour {

	public PlayableCharacter playChar;
	private RectTransform rect;

	// Use this for initialization
	void Start () {
		rect = gameObject.GetComponent<RectTransform> ();
	}
	
	// Update is called once per frame
	void Update () {
		float ratio = (playChar.remainingTime / playChar.powerUpCooldown) * 100f;
		rect.sizeDelta = new Vector2(ratio, rect.sizeDelta.y);
	}
}
