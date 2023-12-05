using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;     // The target to follow (e.g., the player).
    public float smoothing = 5f; // The speed at which the camera follows the target.

    private Vector3 offset;      // The offset from the target.

    void Start()
    {
        // Calculate the initial offset based on the target's position and the camera's position.
        offset = transform.position - target.position;
    }

    void Update()
    {
        if (target != null)
        {
            // Calculate the target position with the offset.
            Vector3 targetPosition = target.position + offset;

            // Smoothly move the camera towards the target position.
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
        }
    }
}
