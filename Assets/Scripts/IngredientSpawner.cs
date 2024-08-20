using System;
using System.Collections.Generic;
using Fusion;
using Oculus.Interaction.HandGrab;
using Oculus.Interaction;
using Oculus.Interaction.OVR.Input;
using UnityEngine;

public class IngredientSpawner : NetworkBehaviour
{
    [SerializeField] private KitchenObjectScriptableObject _ingredient;
    [SerializeField] private List<NetworkObject> _spawnedIngredients;
    private bool spawned = false;

    // public override void Spawned()
    // {
    // if (HasStateAuthority)
    // {
    //     base.Spawned();
    //     Debug.Log("Ingredient Spawner started. Spawning Ingredients");
    //     SpawnIngredient();
    // }
    // }

    //public void SpawnIngredient()
    //{
    //    // if (Object.HasStateAuthority)
    //    //{
    //    _spawnedIngredients.Add(
    //        Runner.Spawn
    //        (
    //            _ingredient.prefab,
    //            transform.position,
    //            Quaternion.identity
    //        )
    //    );
    //    //}
    //}

    //public void RemoveIngredient(NetworkObject ingredient)
    //{
    //    if (HasStateAuthority)
    //    {
    //        Runner.Despawn(ingredient);
    //        _spawnedIngredients.Remove(ingredient);
    //    }
    //}

    [SerializeField] private List<GameObject> playingCard;

    // Reference to the components of the deck of cards
    private Grabbable grabbableChickenSpawner;

    private GrabInteractable grabInteractable;
    private GrabInteractor currentGrabInteractor;

    // Reference to the components of an individual playing card
    private Grabbable invividualChicken;

    private Rigidbody chickenRb;

    // Current index in the shuffled list
    private int currentIndex;

    private void OnEnable()
    {
        grabInteractable = gameObject.GetComponent<GrabInteractable>();
        grabInteractable.WhenSelectingInteractorAdded.Action += HandleSelectingHandGrabInteractorAdded;

        grabbableChickenSpawner = gameObject.GetComponent<Grabbable>();
        grabbableChickenSpawner.WhenPointerEventRaised += Grabbable_WhenPointerEventRaised;
    }

    private void Start()
    {
        // Initialize the current index
        currentIndex = 0;
    }

    private void HandleSelectingHandGrabInteractorAdded(GrabInteractor interactor)
    {
        currentGrabInteractor = interactor;
    }

    private void Grabbable_WhenPointerEventRaised(PointerEvent obj)
    {
        if (obj.Type == PointerEventType.Select)
        {
            NetworkObject newChicken = Runner.Spawn(_ingredient.prefab, transform.position, Quaternion.identity);
            _spawnedIngredients.Add(newChicken);
            newChicken.GetComponentInChildren<Grabbable>().enabled = true;
            HandelChickenSelection(newChicken);
        }

        if (obj.Type == PointerEventType.Unselect)
        {
            Debug.Log("<< Force Realsed success");
        }
    }

    private void HandelChickenSelection(NetworkObject chiecken)
    {
        GrabInteractable chickenInteractable = chiecken.GetComponentInChildren<GrabInteractable>();

        if (currentGrabInteractor == null)
        {
            Debug.Log("<<< Current interactor is null");
            return;
        }

        currentGrabInteractor.ForceRelease();
        currentGrabInteractor.ForceSelect(chickenInteractable);
        return;
    }
}
