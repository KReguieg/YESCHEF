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
	}

	public void StartCookingTimer()
	{
		cookingStarted = true;
	}
}
