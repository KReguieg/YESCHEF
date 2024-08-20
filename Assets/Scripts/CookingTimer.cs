using Oculus.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CookingTimer : MonoBehaviour
{
	private bool cookingStarted;
	private float currentCookingTime;
	private float cookingInterval;
	[SerializeField] private SnapInteractable snapInteractable;
	public UnityEvent OnCookingFinished;
	private AudioTrigger audioTrigger;
	private AudioSource audioSource;

	private void OnEnable()
	{
		snapInteractable.WhenSelectingInteractorViewAdded += HandleSelectingInteractorViewAdded;
	}

	private void HandleSelectingInteractorViewAdded(IInteractorView interactorView)
	{
		StartCookingTimer();
	}

	private void Start()
	{
		cookingStarted = false;
		cookingInterval = 3f;
		audioTrigger = GetComponent<AudioTrigger>();

		audioSource = audioTrigger.GetComponent<AudioSource>();
		audioSource.pitch = 0f;

		audioTrigger.PlayAudio();
	}

	private void Update()
	{
		if (cookingStarted)
		{
			if (currentCookingTime >= cookingInterval)
			{
				currentCookingTime = 0f;
				FinishFoodCooking();
			}
			else
			{
				currentCookingTime += Time.deltaTime;
			}
		}
	}

	public void FinishFoodCooking()
	{
		cookingStarted = false;
		OnCookingFinished.Invoke();
		audioSource.pitch = 0f;
	}

	public void StartCookingTimer()
	{
		cookingStarted = true;
		audioSource.pitch = 1f;
	}
}
