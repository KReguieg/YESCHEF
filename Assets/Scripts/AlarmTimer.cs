using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmTimer : MonoBehaviour
{
	public int time_to_rotate_in_seconds;
	public float angle_to_rotate = 360.0f;
	private float step_angle;
	public bool alarm_timer_activated, game_ended;
	public float timer;
	public ParticleSystem particle;
	public ScoreBehaviour scoreBehaviour;

	// Start is called before the first frame update
	private void Start()
	{
		alarm_timer_activated = false;
		game_ended = false;
		step_angle = angle_to_rotate / time_to_rotate_in_seconds;
		timer = 0;
	}

	// Update is called once per frame
	private void FixedUpdate()
	{
		if (alarm_timer_activated)
		{
			transform.Rotate(Vector3.right, step_angle * Time.fixedDeltaTime);
			timer += Time.fixedDeltaTime;
		}
		if (timer >= time_to_rotate_in_seconds)
		{
			StopTimer();
			scoreBehaviour.AddScore();
			scoreBehaviour.StopScore();
			transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
		}

		if (alarm_timer_activated == true)
		{
			startTimedEvents();
		}
	}

	public void StopTimer()
	{
		alarm_timer_activated = false;
		timer = 0;
		game_ended = true;
		particle.Play();

		//play ring sound
	}

	public void StartTimer()
	{
		if (alarm_timer_activated == false && game_ended == false)
		{
			alarm_timer_activated = true;
		}
	}

	public void startTimedEvents()
	{
		scoreBehaviour.score_active = true;
	}
}
