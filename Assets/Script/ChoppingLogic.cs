using Oculus.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChoppingLogic : MonoBehaviour
{
    [SerializeField] private int numberOfChops = 3;
    [SerializeField] private SnapInteractor snapInteractor;
    public UnityEvent onChopped;

    private int count = 0;
    private bool isSnapped = false;

    private void OnEnable()
    {
        snapInteractor.WhenInteractableSelected.Action += OnSnappedOnChoppingBoard;
        snapInteractor.WhenInteractableUnselected.Action += OnSnappedOffChoppingBoard;
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

        if (count == numberOfChops)
        {
            onChopped.Invoke();
            Debug.Log("Chopping Done");
        }
    }

    public void OnSnappedOnChoppingBoard()
    {
    }
}
