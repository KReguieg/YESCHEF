using Meta.XR.BuildingBlocks;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ColocationStatus : MonoBehaviour
{
    [SerializeField] private SharedSpatialAnchorCore sharedSpatialAnchorCore;
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    private string cloudPermissionMsg =
        "Your headset uses on-device point cloud data to determine its position within your room. " +
        "To expand your headset’s capabilities and enable features like local multiplayer, you’ll need to share point cloud data with Meta. You can turn off point cloud sharing anytime in Settings." +
        "\n\nSettings > Privacy > Device Permissions > Turn on \"Share Point Cloud Data\"";

    private void OnEnable()
    {
        sharedSpatialAnchorCore.OnAnchorCreateCompleted.AddListener(OnAnchorCreateCompleted);
        sharedSpatialAnchorCore.OnAnchorsLoadCompleted.AddListener(OnAnchorLoadCompleted);
        sharedSpatialAnchorCore.OnSharedSpatialAnchorsLoadCompleted.AddListener(OnSharedSpatialAnchorLoad);
        sharedSpatialAnchorCore.OnSpatialAnchorsShareCompleted.AddListener(OnAnchorShare);
    }

    private void OnAnchorLoadCompleted(List<OVRSpatialAnchor> arg0)
    {
        throw new NotImplementedException();
    }

    public void OnAnchorCreateCompleted(OVRSpatialAnchor _, OVRSpatialAnchor.OperationResult result)
    {
        if (result == OVRSpatialAnchor.OperationResult.Failure_SpaceCloudStorageDisabled)
        {
            string message = "<<< On Anchor CreateCompleted" + cloudPermissionMsg;
            Debug.Log(message);
            _textMeshPro.text = message;
            return;
        }

        if (result != OVRSpatialAnchor.OperationResult.Success)
        {
            Debug.Log($"Failed to create the spatial anchor.");
            _textMeshPro.text = "Failed to create the spatial anchor.";
        }
        else
        {
            Debug.Log($"<<<<< Created the spatial anchor.");
            _textMeshPro.text = "Created the spatial anchor.";
            this.gameObject.SetActive(false);
        }
    }

    public void OnAnchorShare(List<OVRSpatialAnchor> _, OVRSpatialAnchor.OperationResult result)
    {
        if (result == OVRSpatialAnchor.OperationResult.Failure_SpaceCloudStorageDisabled)
        {
            string message = "<<< On Anchor share" + cloudPermissionMsg;
            Debug.Log(message);
            _textMeshPro.text = message;

            return;
        }

        if (result != OVRSpatialAnchor.OperationResult.Success)
        {
            Debug.Log($"Failed to share the spatial anchor.");
            _textMeshPro.text = "Failed to share the spatial anchor.";
        }
        else
        {
            Debug.Log($"<<<<< Shared the spatial anchor.");
            _textMeshPro.text = "Shared the spatial anchor.";
        }
    }

    public void OnSharedSpatialAnchorLoad(List<OVRSpatialAnchor> loadedAnchors, OVRSpatialAnchor.OperationResult result)
    {
        if (result == OVRSpatialAnchor.OperationResult.Failure_SpaceCloudStorageDisabled)
        {
            string errorMessage = "<< OnSharedSpatialAnchorLoad" + cloudPermissionMsg;
            Debug.Log(errorMessage);
            _textMeshPro.text = errorMessage;
            return;
        }

        if (result != OVRSpatialAnchor.OperationResult.Success)
        {
            Debug.Log($"Failed to Load the spatial anchor.");
            _textMeshPro.text = "Failed to Load the spatial anchor.";
        }
        else
        {
            Debug.Log($"<<<<< Loaded the spatial anchor.");
            _textMeshPro.text = "Loaded the spatial anchor";
            this.gameObject.SetActive(false);
        }
        if (loadedAnchors == null || loadedAnchors.Count == 0)
        {
            Debug.Log($"Failed to load the spatial anchor(s).");
            _textMeshPro.text = "Failed to load the spatial anchor(s)";
        }
    }
}
