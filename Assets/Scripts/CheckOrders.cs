using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckOrders : MonoBehaviour
{
    [SerializeField] private OrderManager orderManager;

    private PlateInfo plateInfo;
    private string orderString1;
    private string orderString2;

    private bool orderFound = false;
    private int orderNumber;

    private UnityEvent onRightOrder;
    private UnityEvent onWrongOrder;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Plate"))
            return;
        orderString1 = plateInfo.food1 + "\n" + plateInfo.food2;
        orderString2 = plateInfo.food2 + "\n" + plateInfo.food1;

        CheckFromManager();
    }

    public void CheckFromManager()
    {
        var GO = orderManager.Orders;
        foreach (var o in GO)
        {
            string order = o.GetComponent<OrderObject>().OrderText.text;
            if (order == orderString1 || order == orderString2)
            {
                orderFound = true;
                orderNumber = o.GetComponent<OrderObject>().OrderNumber;
                orderManager.ResolveOrder(o);
                onRightOrder?.Invoke();
                break;
            }
        }
        onWrongOrder?.Invoke();
    }
}
