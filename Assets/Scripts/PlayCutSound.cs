using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCutSound : MonoBehaviour
{
	private AudioTrigger audioTrigger;

	private void Start()
	{
		audioTrigger = GetComponent<AudioTrigger>();
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Cut"))
		{
			audioTrigger.PlayAudio();
		}
	}
}
