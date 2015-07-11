using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class TimerUI : MonoBehaviour {
	
	private Text m_timerText;
	public float m_remainingTime;
	public int m_timeElapsedSpeed = 1;
	public GameManager gameManager;
	
	// Use this for initialization
	void Start () {
		m_timerText = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		UpdatingTime ();
	}

	
	void UpdatingTime() {
		m_remainingTime -= (Time.fixedDeltaTime * m_timeElapsedSpeed);
		if (m_remainingTime > 0) {
			m_timerText.text = TimeToString ();
		}
		else {
			m_timerText.text = "00:00:00";
			gameManager.EndGame();
		}
	}
	
	String TimeToString() {
		int minutes = (int)Mathf.Floor (m_remainingTime / 60);
		int seconds = (int)(m_remainingTime % 60);
		int mili = (int)((m_remainingTime * 100) % 100);
		
		return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, mili);
	}
}
