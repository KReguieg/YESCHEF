using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBehaviour : MonoBehaviour
{
	public bool score_active;
	public int current_score, start_score = 0, score_per_shot;

	[SerializeField]
	public TMP_Text[] currentScoreText;

	public AlarmTimer alarmTimer;

	public TextAnimBehaviour textAnimBehaviour;

	private void Start()
	{
		current_score = start_score;

		currentScoreText[0].text = current_score.ToString();
	}

	private void Update()
	{
		//score_active = alarmTimer.alarm_timer_activated;          // might want
		currentScoreText[0].text = current_score.ToString();

		print(score_active);
	}

	public void AddScore()
	{
		if (score_active == true)
		{
			print("added score");
			current_score += score_per_shot;
			textAnimBehaviour.ScoreAddText(score_per_shot);
		}
	}

	public void StopScore()
	{
		score_active = false;
	}
}
