using Fusion;
using Meta.XR.BuildingBlocks;
using Meta.XR.MRUtilityKit;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.ProBuilder.AutoUnwrapSettings;

public class KitchenSpawner : NetworkBehaviour
{
    [SerializeField] private GameObject ghostkitchenGamePre;
    [SerializeField] private GameObject kitchenGamePre;

    private SharedSpatialAnchorCore sharedSpatialAnchorCore;

    private GameObject newGhostKitchenObj;
    private GameObject newKitchenObj;

    //private NetworkObject newGhostKitchenObj;
    //private NetworkObject newKitchenObj;
    private OVRSpatialAnchor spatialAnchor;

    private void Update()
    {
        if (OVRInput.Get(OVRInput.RawButton.A))
        {
            //if (newKitchenObj == null)
            //{
            //    newKitchenObj = Runner.Spawn(kitchenGamePre);
            //    newKitchenObj.transform.SetParent(spatialAnchor.transform);
            //    newKitchenObj.transform.localPosition = newGhostKitchenObj.transform.localPosition;
            //    newKitchenObj.transform.localRotation = newGhostKitchenObj.transform.localRotation;

            //    Runner.Despawn(newGhostKitchenObj);

            Instantiate(kitchenGamePre, newGhostKitchenObj.transform.position, newGhostKitchenObj.transform.rotation);
            newGhostKitchenObj.SetActive(false);
        }
    }

    public override void Spawned()
    {
        base.Spawned();
        //if (Object.HasStateAuthority)
        //{
        sharedSpatialAnchorCore = FindObjectOfType<SharedSpatialAnchorCore>();
        sharedSpatialAnchorCore.OnAnchorCreateCompleted.AddListener(OnAnchorCreateCompleted);
        sharedSpatialAnchorCore.OnSharedSpatialAnchorsLoadCompleted.AddListener(OnSharedSpatialAnchorLoad);

        //}
    }

    public void OnSharedSpatialAnchorLoad(List<OVRSpatialAnchor> loadedAnchors, OVRSpatialAnchor.OperationResult result)
    {
        if (result == OVRSpatialAnchor.OperationResult.Failure_SpaceCloudStorageDisabled)
        {
            string errorMessage = "<< OnSharedSpatialAnchorLoad";
            Debug.Log(errorMessage);
            return;
        }

        if (result != OVRSpatialAnchor.OperationResult.Success)
        {
            Debug.Log($"Failed to Load the spatial anchor.");
        }
        else
        {
            Debug.Log($"<<<<< Loaded the spatial anchor.");
            spatialAnchor = FindAnyObjectByType<OVRSpatialAnchor>();
            var floor = MRUK.Instance?.GetCurrentRoom()?.FloorAnchor;
            //newGhostKitchenObj = Runner.Spawn(ghostkitchenGamePre, floor.transform.position, Quaternion.identity);
            //newGhostKitchenObj.transform.parent = spatialAnchor.transform;

            newGhostKitchenObj = Instantiate(ghostkitchenGamePre, floor.transform.position, Quaternion.identity);
        }
        if (loadedAnchors == null || loadedAnchors.Count == 0)
        {
            Debug.Log($"Failed to load the spatial anchor(s).");
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
            Debug.Log($"Failed to create the spatial anchor.");
        }
        else
        {
            spatialAnchor = FindAnyObjectByType<OVRSpatialAnchor>();
            var floor = MRUK.Instance?.GetCurrentRoom()?.FloorAnchor;
            //newGhostKitchenObj = Runner.Spawn(ghostkitchenGamePre, floor.transform.position, Quaternion.identity);
            //newGhostKitchenObj.transform.parent = spatialAnchor.transform;

            newGhostKitchenObj = Instantiate(ghostkitchenGamePre, floor.transform.position, Quaternion.identity);

            Debug.Log($"<<<<< Created the spatial anchor.");
            var anchors = MRUK.Instance?.GetCurrentRoom()?.Anchors;
            //foreach (var anchor in anchors)
            //{
            //    if (anchor.Label == MRUKAnchor.SceneLabels.TABLE)
            //        Debug.Log("Table Found");

            //    if (newGhostKitchenPrefab == null)
            //        newGhostKitchenPrefab = Instantiate(ghostkitchenGameObject, anchor.transform.position, anchor.transform.rotation);
            //}
        }
    }
}
