using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateInfo : MonoBehaviour
{
    [SerializeField] private SnapInteractable item1;
    [SerializeField] private SnapInteractable item2;

    public string food1;
    public string food2;

    private void OnEnable()
    {
        item1.WhenSelectingInteractorAdded.Action += WhenSelectingInteractorAdded_Action;
        item2.WhenSelectingInteractorAdded.Action += WhenSelectingInteractorAdded_Action1;
    }

    private void WhenSelectingInteractorAdded_Action1(SnapInteractor obj)
    {
        food2 = obj.transform.parent.tag;
        obj.gameObject.transform.SetParent(this.transform, true);
        obj.GetComponent<SnapInteractable>().enabled = false;
        obj.transform.parent.GetComponentInChildren<Grabbable>().enabled = false;
    }

    private void WhenSelectingInteractorAdded_Action(SnapInteractor obj)
    {
        food1 = obj.transform.parent.tag;
        obj.gameObject.transform.SetParent(this.transform, true);
        obj.GetComponent<SnapInteractable>().enabled = false;
        obj.transform.parent.GetComponentInChildren<Grabbable>().enabled = false;
    }
}
