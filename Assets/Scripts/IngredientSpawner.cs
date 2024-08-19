using Fusion;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class IngredientSpawner : NetworkBehaviour
{
	[SerializeField] private KitchenObjectScriptableObject[] _possibleIngredients;
	[SerializeField] private List<NetworkObject> _ingredients;
	[SerializeField] private Transform[] _ingredientSpawnTransforms;

	[SerializeField] private int _ingredientLimit;
	[SerializeField] private int randomIngredientPick;

	private GameObject _ingredientPrefab;

	public bool spawned = false;
	public bool hasStateAuthority;

	public override void Spawned()
	{
		hasStateAuthority = HasStateAuthority;

		if (HasStateAuthority)
		{
			base.Spawned();
			Debug.Log("Ingredient Spawner started. Spawning Ingredients.");
			for (var i = 0; i < _ingredientLimit; i++)
			{
				SpawnIngredient(i);
			}

			spawned = true;
		}
	}

	private void Update()
	{
		if (spawned)
		{
			if (_ingredients.Count < _ingredientLimit)
			{
				SpawnIngredient(GetEmptyIngredientSlots());
			}

			if (Input.GetKeyDown(KeyCode.P))
			{
				SpawnIngredient(GetEmptyIngredientSlots());
			}

			if (Input.GetKeyDown(KeyCode.R))
			{
				RemoveIngredient(_ingredients[^2]);
			}
		}
	}

	public int GetEmptyIngredientSlots()
	{
		for (var i = 0; i < _ingredients.Count; i++)
		{
			if (_ingredients[i] == null)
				return i;
		}

		return 0;
	}

	public void SpawnIngredient(int ingredientPositionNumber)
	{
		if (Object.HasStateAuthority)
		{
			randomIngredientPick = Random.Range(0, _ingredients.Count - 1);
			Debug.Log(randomIngredientPick);
			var ingredientSO = _possibleIngredients[randomIngredientPick];
			var orderPos = _ingredientSpawnTransforms[ingredientPositionNumber].position;
			_ingredients.Add(
				Runner.Spawn
				(
					ingredientSO.prefab,
					orderPos,
					Quaternion.identity,
					inputAuthority: null,
					(Runner, NO) => Debug.Log("Spawning kitchen object")//NO.GetComponent<KitchenObject>().Init(orderString, ingredientPositionNumber)
				)
			);
		}
	}

	public void RemoveIngredient(NetworkObject ingredient)
	{
		if (HasStateAuthority)
		{
			Runner.Despawn(ingredient);
			_ingredients.Remove(ingredient);
			//_ingredients[ingredient.GetComponent<OrderObject>().OrderNumber] = null;
		}
	}
}
