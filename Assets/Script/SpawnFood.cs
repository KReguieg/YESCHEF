using Fusion;
using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : NetworkBehaviour
{
    public float timer, interval;
    public bool noCoolDown, noExitingBall;
    public int numberOfBallsInSpawner;

    [SerializeField] private KitchenObjectScriptableObject _ingredient;
    [SerializeField] private List<NetworkObject> _spawnedIngredients;

    // Start is called before the first frame update
    private void Start()
    {
        noExitingBall = false;
        noCoolDown = true;
        timer = 0.0f;
        interval = 1.5f;
    }

    // Update is called once per frame
    private void Update()
    {
        CheckCooldown();
        CheckForExistingBall();
    }

    public void CheckCooldown()
    {
        if (!noCoolDown)
        {
            if (timer >= interval)
            {
                timer -= interval;
                noCoolDown = true;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }

    public void CheckForExistingBall()
    {
        if (numberOfBallsInSpawner == 0)
        {
            noExitingBall = true;
        }
        else
        {
            noExitingBall = false;
        }
    }

    [ContextMenu("Spawn Food")]
    public void SpawnFoodObject()
    {
        Debug.Log("<<< Spawned food item");

        if (noCoolDown && noExitingBall)
        {
            Debug.Log("<<< Spawned food item");
            NetworkObject newChicken = Runner.Spawn(_ingredient.prefab, transform.position, Quaternion.identity);
            _spawnedIngredients.Add(newChicken);
            noCoolDown = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("<< ON trigger exit");
        if (other.CompareTag("Chicken") || other.CompareTag("Potato") || other.CompareTag("Plate"))
        {
            Debug.Log("<< ON trigger exit" + _ingredient.objectName.ToString());

            SpawnFoodObject();
        }
    }
}
