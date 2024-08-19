using Fusion;
using Oculus.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SnappingLogic : NetworkBehaviour
{
	private enum FoodState
	{
		RAW = 0,
		CHOPPED = 1,
		FRIED = 2,
		PLATED = 3
	}

	[SerializeField] private int numberOfChops = 3;
	[SerializeField] private SnapInteractor snapInteractor;
	[SerializeField] private List<GameObject> foodObjects = new List<GameObject>();
	[SerializeField] private TagSet tagSet;

	private CookingTimer cookingTimer;

	public UnityEvent onChopped;
	public UnityEvent onFried;

	private FoodState state;

	[Networked]
	[OnChangedRender(nameof(CounterChanged))]
	public int Counter { get; set; }

	private void CounterChanged(NetworkBehaviourBuffer previous)
	{
		var prevValue = GetPropertyReader<int>(nameof(Counter)).Read(previous);
		Log.Info($"counter changed: {Counter}, prev: {prevValue}");

		if (prevValue == 0 && Counter == 1)
		{
			ChangeFoodVisual(FoodState.CHOPPED);
		}
		if (prevValue == 1 && Counter == 2)
		{
			ChangeFoodVisual(FoodState.FRIED);
		}
	}

	private int count = 0;
	private bool isSnapped = false;
	private bool canBeChopped;
	private bool canBeCooked;
	private bool canBePlated;
	private bool canBeServed;

	private void OnEnable()
	{
		snapInteractor.WhenInteractableSelected.Action += OnSnapped;
		snapInteractor.WhenInteractableUnselected.Action += OnSnappedOff;
		Counter = 0;
		state = FoodState.RAW;
	}

	private void OnSnappedOff(SnapInteractable obj)
	{
		Debug.Log("UnSnapped");
		isSnapped = false;
	}

	private void OnSnapped(SnapInteractable interactable)
	{
		Debug.Log("Snapped");
		isSnapped = true;
		switch (state)
		{
			case FoodState.RAW:
				canBeChopped = true;
				break;

			case FoodState.CHOPPED:
				canBeCooked = true;
				interactable.GetComponent<CookingTimer>().OnCookingFinished.AddListener(OnFinishedCooking);
				break;

			case FoodState.FRIED:
				canBePlated = true;
				break;

			case FoodState.PLATED:
				canBeServed = true;
				break;

			default:
				break;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.CompareTag("Knife") && canBeChopped)
			count++;

		if (count == numberOfChops && state == FoodState.RAW)
		{
			OnFinishedChopped();
		}
	}

	private void OnFinishedChopped()
	{
		state = FoodState.CHOPPED;
		ChangeFoodVisual(state);
		tagSet.tag = state.ToString();
		Counter++;
		onChopped.Invoke();
		Debug.Log("Chopping Done");
		count = 0;
	}

	private void OnFinishedCooking()
	{
		state = FoodState.FRIED;
		ChangeFoodVisual(state);
		tagSet.tag = state.ToString();
		Counter++;
		onFried.Invoke();
		Debug.Log("Frying Done");
	}

	private void ChangeFoodVisual(FoodState newState)
	{
		if (state == FoodState.RAW)
		{
			foodObjects[0].gameObject.SetActive(false);
			foodObjects[1].gameObject.SetActive(true);
		}
		else if (state == FoodState.CHOPPED)
		{
			foodObjects[1].gameObject.SetActive(false);
			foodObjects[2].gameObject.SetActive(true);
		}
	}
}
