using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VisorFollower : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float distance = 3.0f;
    private bool isCentered = false;

    // Built-in Unity *Magic*
    private void OnBecameInvisible()
    {
        isCentered = false;
    }

    private void Update()
    {
        if (!isCentered)
        {
            Vector3 targetPosition = FindTargetPosition();
            // Move just a little bit at a time
            MoveTowards(targetPosition);
            if (ReachedPosition(targetPosition))
                isCentered = true;
        }
        // Find the position we need to be at
        // If we've reached the position, don't do anymore work
    }

    private Vector3 FindTargetPosition()
    {
        return cameraTransform.position + (cameraTransform.forward * distance);
    }

    // Let's get a position infront of the player's camera
    private void MoveTowards(Vector3 targetPosition)
    {
        transform.position += (targetPosition - transform.position) * 0.025f;
    }

    // Instead of a tween, that would need to be constantly restarted
    private bool ReachedPosition(Vector3 targetPosition)
    {
        return Vector3.Distance(targetPosition, transform.position) < 0.1f;
    }

    // Simple distance check, can be replaced if you wish
}
