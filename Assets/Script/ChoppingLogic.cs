using Fusion;
using Oculus.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChoppingLogic : NetworkBehaviour
{
    private enum FoodState
    {
        RAW = 0,
        CHOPPED = 1,
        FRIED = 2
    }

    [SerializeField] private int numberOfChops = 3;
    [SerializeField] private SnapInteractor snapInteractor;
    [SerializeField] private List<GameObject> foodObjects = new List<GameObject>();
    [SerializeField] private TagSet tagSet;

    public UnityEvent onChopped;
    public UnityEvent onFried;

    private FoodState state = FoodState.RAW;

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

    private void OnEnable()
    {
        snapInteractor.WhenInteractableSelected.Action += OnSnappedOnChoppingBoard;
        snapInteractor.WhenInteractableUnselected.Action += OnSnappedOffChoppingBoard;
        Counter = 0;
        // listen to jads script
    }

    private void OnSnappedOffChoppingBoard(SnapInteractable obj)
    {
        Debug.Log("UnSnapped");
        isSnapped = false;
    }

    private void OnSnappedOnChoppingBoard(SnapInteractable interactable)
    {
        Debug.Log("Snapped");
        isSnapped = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Knife") && isSnapped)
            count++;

        if (count == numberOfChops && state == FoodState.RAW)
        {
            state = FoodState.CHOPPED;
            ChangeFoodVisual(state);
            tagSet.tag = state.ToString();
            Counter++;
            onChopped.Invoke();
            Debug.Log("Chopping Done");
            count = 0;
        }
    }

    private void OnFinishedFried()
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
        else
        {
            foodObjects[1].gameObject.SetActive(false);
            foodObjects[2].gameObject.SetActive(true);
        }
    }
}
