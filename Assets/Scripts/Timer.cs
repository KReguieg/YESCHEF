using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Fusion;
using Oculus.Interaction;

public class Timer : NetworkBehaviour
{
	[Rpc(RpcSources.All, RpcTargets.All)]
	public void RPC_StartTimer()
	{
		StartTimer();
	}

	public void CallTimerRPC()
	{
		RPC_StartTimer();
	}

	public bool timerActive;
	private float currentTime;

	[SerializeField]
	private int startSeconds;

	public TMP_Text currentTimeText;
	private TimeSpan time;

	private string minutes_text, seconds_text;

	public AlarmTimer alarmTimer;
	private AudioTrigger audioTrigger;
	public AudioTrigger audioTriggerMusic;
	public AudioSource audioSource;

	private void Start()
	{
		startSeconds = alarmTimer.time_to_rotate_in_seconds;
		timerActive = false;
		//currentTimeText.text = "";
		currentTime = startSeconds + 1;

		currentTime = currentTime - Time.fixedDeltaTime;
		time = TimeSpan.FromSeconds(currentTime);
		FixFormat();
		currentTimeText.text = minutes_text + ":" + seconds_text;
		audioTrigger = GetComponent<AudioTrigger>();
	}

	private void Update()
	{
		timerActive = alarmTimer.alarm_timer_activated;
	}

	private void FixedUpdate()
	{
		if (timerActive == true)
		{
			currentTime = currentTime - Time.fixedDeltaTime;
			time = TimeSpan.FromSeconds(currentTime);
			FixFormat();
			currentTimeText.text = minutes_text + ":" + seconds_text;
		}

		if (alarmTimer.game_ended == true)
		{
			currentTimeText.text = "00:00";
		}
	}

	public void StartTimer()
	{
		timerActive = true;
		alarmTimer.StartTimer();
		audioTriggerMusic.PlayAudio();
	}

	public void StopTimer()
	{
		timerActive = false;
		audioTrigger.PlayAudio();
		audioSource.pitch = 0; //stops audio
	}

	public void FixFormat()
	{
		if (time.Minutes < 10)
		{
			minutes_text = "0" + time.Minutes.ToString();
		}
		else
		{
			minutes_text = time.Minutes.ToString();
		}
		if (time.Seconds < 10)
		{
			seconds_text = "0" + time.Seconds.ToString();
		}
		else
		{
			seconds_text = time.Seconds.ToString();
		}
	}
}
