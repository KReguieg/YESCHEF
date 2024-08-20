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
        // if (Object.HasStateAuthority)
        //{
        _spawnedIngredients.Add(
            Runner.Spawn
            (
                _ingredient.prefab,
                transform.position,
                Quaternion.identity
            )
        );
        //}
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
