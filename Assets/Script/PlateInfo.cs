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
        var networkObject = obj.GetComponentInParent<NetworkObject>();
        Debug.Log( "WhenSelectingInteractorAdded_Action " +networkObject );
        RPC_DisableFunctionality1(networkObject);
    }

    private void WhenSelectingInteractorAdded_Action1(SnapInteractor obj)
    {
        var networkObject = obj.GetComponentInParent<NetworkObject>();
        Debug.Log( "WhenSelectingInteractorAdded_Action " +networkObject );
        RPC_DisableFunctionality2(networkObject);
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_DisableFunctionality1(NetworkObject networkObject)
    {
        Debug.Log( "RPC_DisableFunctionality1 " +networkObject );
        food1 = networkObject.transform.tag;
        networkObject.gameObject.transform.SetParent(this.transform, true);
        networkObject.GetComponentInChildren<SnapInteractor>().enabled = false;
        networkObject.transform.GetComponentInChildren<Grabbable>().enabled = false;
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_DisableFunctionality2(NetworkObject networkObject)
    {
        Debug.Log( "RPC_DisableFunctionality2 " +networkObject );
        food2 = networkObject.transform.tag;
        networkObject.gameObject.transform.SetParent(this.transform, true);
        networkObject.GetComponentInChildren<SnapInteractor>().enabled = false;
        networkObject.transform.GetComponentInChildren<Grabbable>().enabled = false;
    }
}
