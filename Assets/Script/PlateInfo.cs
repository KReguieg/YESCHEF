using Fusion;
using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateInfo : NetworkBehaviour
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

    private void WhenSelectingInteractorAdded_Action(SnapInteractor obj)
    {
        RPC_DisableFunctionality1(obj.GetComponentInParent<NetworkObject>());
    }

    private void WhenSelectingInteractorAdded_Action1(SnapInteractor obj)
    {
        RPC_DisableFunctionality2(obj.GetComponentInParent<NetworkObject>());
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_DisableFunctionality1(NetworkObject networkObject)
    {
        food1 = networkObject.transform.parent.tag;
        networkObject.gameObject.transform.SetParent(this.transform, true);
        networkObject.GetComponent<SnapInteractor>().enabled = false;
        networkObject.transform.parent.GetComponentInChildren<Grabbable>().enabled = false;
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_DisableFunctionality2(NetworkObject networkObject)
    {
        food2 = networkObject.transform.parent.tag;
        networkObject.gameObject.transform.SetParent(this.transform, true);
        networkObject.GetComponent<SnapInteractor>().enabled = false;
        networkObject.transform.parent.GetComponentInChildren<Grabbable>().enabled = false;
    }
}
