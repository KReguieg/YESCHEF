using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingTimer : MonoBehaviour
{
	private bool cookingStarted;
	private float currentCookingTime;
	private float cookingInterval;

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
		//Invoke(); //call ashray's function to change state of food;
	}

	public void StartCookingTimer()
	{
		cookingStarted = true;
	}
}
