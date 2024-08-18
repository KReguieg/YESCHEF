using System;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class OrderManager : NetworkBehaviour
{
    [SerializeField] private Queue<KitchenObjectScriptableObject> _orderRecipes;

    public override void Spawned()
    {
        base.Spawned();
        Debug.Log("OrderManager startet. Creating orders.");
    }
}
