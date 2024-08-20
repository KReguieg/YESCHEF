using System;
using System.Collections.Generic;
using Fusion;
using Oculus.Interaction.OVR.Input;
using UnityEngine;


public class IngredientSpawner : NetworkBehaviour
{
    [SerializeField] private KitchenObjectScriptableObject _ingredient;
    [SerializeField] private List<NetworkObject> _spawnedIngredients;
    private bool spawned = false;

    private void Update()
    {
        if (OVRInput.Get(OVRInput.RawButton.B))
        {
            if (!spawned)
            {
                if (HasStateAuthority)
                {
                    SpawnIngredient();
                    spawned = true;
                }
            }
        }
    }

    // public override void Spawned()
    // {
    // if (HasStateAuthority)
    // {
    //     base.Spawned();
    //     Debug.Log("Ingredient Spawner started. Spawning Ingredients");
    //     SpawnIngredient();
    // }
    // }

    public void SpawnIngredient()
    {
        if (Object.HasStateAuthority)
        {
            _spawnedIngredients.Add(
                Runner.Spawn
                (
                    _ingredient.prefab,
                    transform.position,
                    Quaternion.identity,
                    inputAuthority: null,
                    (Runner, NO) =>
                        Debug.Log(
                            "Spawning kitchen object") //NO.GetComponent<KitchenObject>().Init(orderString, ingredientPositionNumber)
                )
            );
        }
    }

    public void RemoveIngredient(NetworkObject ingredient)
    {
        if (HasStateAuthority)
        {
            Runner.Despawn(ingredient);
            _spawnedIngredients.Remove(ingredient);
        }
    }
}