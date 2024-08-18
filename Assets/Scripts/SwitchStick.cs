using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchStick : MonoBehaviour
{
	public GameObject sword;
	public GameObject stick_mesh_object;

	//Respawn timer
	public float timer, interval;
	public bool timer_active, transformed;

	// Start is called before the first frame update
	void Start()
    {
		timer = 0;
		timer_active = false;
		transformed = false;
	}

    // Update is called once per frame
    void Update()
    {
		if (timer_active)
		{
			if (timer >= interval)
			{
				timer_active = false;
				timer -= interval;
				stick_mesh_object.SetActive(!stick_mesh_object.activeSelf);
				transformed = !transformed;
				sword.SetActive(transformed);
			}
			else
			{
				timer += Time.deltaTime;
			}
		}
	}
	public void ActivateTimer()
	{
		timer = 0;
		timer_active = true;
	}
	public void ResetStopTimer()
	{
			timer = 0;
			timer_active = false;
	}
}