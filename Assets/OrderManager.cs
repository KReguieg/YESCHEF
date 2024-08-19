using System;
using System.Collections.Generic;
using Fusion;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;


public class OrderManager : NetworkBehaviour
{
    [SerializeField] private OrderRecipeScriptableObject[] _possibleOrders;
    [SerializeField] private List<NetworkObject> _orders;

    [SerializeField] private int orderLimit;
    [SerializeField] private GameObject _orderPrefab;
    [SerializeField] private int orderNumber;
    public bool spawned = false;
    public bool hasStateAuthority;


    public override void Spawned()
    {
        hasStateAuthority = HasStateAuthority;
        
        if (HasStateAuthority)
        {
            base.Spawned();
            Debug.Log("OrderManager started. Creating orders.");
            for (var i = 0; i < orderLimit; i++)
            {
                PlaceOrder();
            }

            spawned = true;
        }
    }

    private void Update()
    {
        if (spawned)
        {
            if (_orders.Count < orderLimit)
            {
                PlaceOrder();
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                PlaceOrder();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                ResolveOrder(_orders[^1]);
            }
        }
    }

    public void PlaceOrder()
    {
        if (HasStateAuthority)
        {
            orderNumber = Random.Range(0, 3);
            string orderString = "";
            foreach (var obj in _possibleOrders[orderNumber].order)
            {
                orderString += obj.objectName + "\n";
            }

            var networkObject = Runner.Spawn
            (
                _orderPrefab,
                Vector3.zero,
                Quaternion.identity,
                inputAuthority: null,
                (Runner, NO) => NO.GetComponent<OrderObject>().Init(orderString)
            );
            _orders.Add(networkObject);
        }
    }

    public void ResolveOrder(NetworkObject order)
    {
        if (HasStateAuthority)
        {
            Runner.Despawn(order);
            _orders.Remove(order);
        }
    }
}