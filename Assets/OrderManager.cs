using System.Collections.Generic;
using System.Linq;
using Fusion;
using UnityEngine;
using Random = UnityEngine.Random;

public class OrderManager : NetworkBehaviour
{
    [SerializeField] private OrderRecipeScriptableObject[] _possibleOrders;
    [SerializeField] private List<NetworkObject> _orders;
    [SerializeField] private Transform[] _orderTransforms;

    [SerializeField] private int _orderLimit;
    [SerializeField] private GameObject _orderPrefab;
    [SerializeField] private int randomOrderPick;
    public bool spawned = false;
    public bool hasStateAuthority;

    public List<NetworkObject> Orders { get => _orders; }

    public override void Spawned()
    {
        hasStateAuthority = HasStateAuthority;

        if (HasStateAuthority)
        {
            base.Spawned();
            Debug.Log("OrderManager started. Creating orders.");
            for (var i = 0; i < _orderLimit; i++)
            {
                PlaceOrder(i);
            }

            spawned = true;
        }
    }

    private void Update()
    {
        if (spawned)
        {
            if (_orders.Count < _orderLimit)
            {
                PlaceOrder(GetEmptyOrderSlots());
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                PlaceOrder(GetEmptyOrderSlots());
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                ResolveOrder(_orders[^2]);
            }
        }
    }

    public int GetEmptyOrderSlots()
    {
        for (var i = 0; i < _orders.Count; i++)
        {
            if (_orders[i] == null)
                return i;
        }

        return 0;
    }

    public void PlaceOrder(int orderPositionNumber)
    {
        if (Object.HasStateAuthority)
        {
            Debug.Log(orderPositionNumber);
            randomOrderPick = Random.Range(0, 3);
            string orderString = "";
            foreach (var obj in _possibleOrders[randomOrderPick].order)
            {
                orderString += obj.objectName + "\n";
            }

            var orderPos = _orderTransforms[orderPositionNumber].position;
            _orders.Add(
                Runner.Spawn
                (
                    _orderPrefab,
                    orderPos,
                    Quaternion.identity,
                    inputAuthority: null,
                    (Runner, NO) => NO.GetComponent<OrderObject>().Init(orderString, orderPositionNumber)
                )
            );
        }
    }

    public void ResolveOrder(NetworkObject order)
    {
        if (HasStateAuthority)
        {
            Runner.Despawn(order);
            _orders[order.GetComponent<OrderObject>().OrderNumber] = null;
        }
    }
}
