using Fusion;
using Meta.XR.BuildingBlocks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchedSpawner : NetworkBehaviour
{
    [SerializeField] private GameObject ghostkitchenGamePre;
    [SerializeField] private GameObject kitchenGamePre;

    private SharedSpatialAnchorCore sharedSpatialAnchorCore;

    private GameObject newGhostKitchenObj;
    private NetworkObject newKitchenObj;
    private OVRSpatialAnchor spatialAnchor;

    private void Update()
    {
        if (OVRInput.Get(OVRInput.RawButton.A))
        {
            if (newKitchenObj == null)
            {
                newKitchenObj = Runner.Spawn(kitchenGamePre, newGhostKitchenObj.transform.localPosition, newGhostKitchenObj.transform.localRotation);
                newKitchenObj.transform.SetParent(spatialAnchor.transform);
                Debug.Log("<<< Ghost pos" + newGhostKitchenObj.transform.position);
                Debug.Log("<<<< Room pos" + newGhostKitchenObj.transform.localPosition);
                Debug.Log("<<< New kit pos" + newKitchenObj.transform.position);
                Debug.Log("<<< New kit pos room" + newKitchenObj.transform.localPosition);

                newGhostKitchenObj.SetActive(false);
            }
        }
    }

    public override void Spawned()
    {
        base.Spawned();
        if (Object.HasStateAuthority)
        {
            sharedSpatialAnchorCore = FindObjectOfType<SharedSpatialAnchorCore>();

            sharedSpatialAnchorCore.OnAnchorCreateCompleted.AddListener(OnAnchorCreateCompleted);
        }
    }

    private void OnAnchorCreateCompleted(OVRSpatialAnchor arg0, OVRSpatialAnchor.OperationResult result)
    {
        if (result == OVRSpatialAnchor.OperationResult.Failure_SpaceCloudStorageDisabled)
        {
            string message = "<<< On Anchor CreateCompleted";
            Debug.Log(message);
            return;
        }

        if (result != OVRSpatialAnchor.OperationResult.Success)
        {
            Debug.Log($"Failed to create the spatial anchor");
        }
        else
        {
            spatialAnchor = FindAnyObjectByType<OVRSpatialAnchor>();
            newGhostKitchenObj = Instantiate(ghostkitchenGamePre, transform.position, Quaternion.identity);
            newGhostKitchenObj.transform.parent = spatialAnchor.transform;

            Debug.Log($"<<<<< Created the ghost room.");
        }
    }
}
