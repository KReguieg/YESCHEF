using Oculus.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckOrders : MonoBehaviour
{
    [SerializeField] private OrderManager orderManager;
    [SerializeField] private SnapInteractable snapInteractable;

    private PlateInfo plateInfo;
    private string orderString1;
    private string orderString2;

    private bool orderFound = false;
    private int orderNumber;

    public UnityEvent onRightOrder;
    public UnityEvent onWrongOrder;

    private void OnEnable()
    {
        snapInteractable.WhenSelectingInteractorAdded.Action += WhenSelectingInteractorAdded_Action;
    }

    private void WhenSelectingInteractorAdded_Action(SnapInteractor interactor)
    {
        plateInfo = interactor.GetComponentInParent<PlateInfo>();
        orderString1 = plateInfo.food1 + "\n" + plateInfo.food2;
        orderString2 = plateInfo.food2 + "\n" + plateInfo.food1;

        Debug.Log("<<< order" + orderString1);
        Debug.Log("<<<< order 2 " + orderString2);
        CheckFromManager();
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (!other.CompareTag("Plate"))
    //        return;
    //    orderString1 = plateInfo.food1 + "\n" + plateInfo.food2;
    //    orderString2 = plateInfo.food2 + "\n" + plateInfo.food1;

    //    CheckFromManager();
    //}

    public void CheckFromManager()
    {
        var GO = orderManager.Orders;
        foreach (var o in GO)
        {
            string order = o.GetComponent<OrderObject>().OrderText.text;
            Debug.Log("<<< from Manager" + order);
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
