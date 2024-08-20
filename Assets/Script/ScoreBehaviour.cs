using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using TMPro;
using UnityEngine;

public class ScoreBehaviour : NetworkBehaviour
{
	public static ScoreBehaviour Instance;
	
	public bool score_active;
	public int current_score, start_score = 0, score_per_shot;

	
	
	[Networked, OnChangedRender(nameof(OnScoreChanged))]
	public int NetworkedScore { get; set; }

	[SerializeField]
	public TMP_Text[] currentScoreText;

	public AlarmTimer alarmTimer;

	public TextAnimBehaviour textAnimBehaviour;

	private void Start()
	{
		Instance = this;
		current_score = start_score;
		currentScoreText[0].text = current_score.ToString();
	}

	private void Update()
	{
		//score_active = alarmTimer.alarm_timer_activated;          // might want
		currentScoreText[0].text = current_score.ToString();
	}

	public void AddScore()
	{
		if (score_active)
		{
			print("added score");
			current_score += score_per_shot;
			NetworkedScore = current_score;
		}
	}

	public void StopScore()
	{
		score_active = false;
	}

	public void ExecuteAddScoreRPC()
	{
		RPC_AddScore();
	}
	
	[Rpc(RpcSources.All, RpcTargets.StateAuthority)]
	public void RPC_AddScore()
	{
		AddScore();
	}

	public void OnScoreChanged()
	{
		//change text
		currentScoreText[0].text = NetworkedScore.ToString();
	}
}
