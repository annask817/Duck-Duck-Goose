using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public Transform target;         // The target the camera is following (duck)
    public float smoothSpeed = 0.125f;   // Speed at which the camera follows
    public float minDistance = 3f;      // Minimum distance from our duck
    public float collisionRadius = 0.5f;  // Radius around the duck to check for collisions
    public LayerMask collisionLayer;   // Determines what type of obstacles

    private Vector3 desiredPosition; //Will be used down below

    void Update() {
        Vector3 direction = transform.position - target.position;  // Calculate the direction from the camera to the target
        desiredPosition = target.position + direction.normalized * minDistance;  // Set desired position based on the distance

        RaycastHit hit;
        if (Physics.Raycast(target.position, direction, out hit, minDistance + collisionRadius, collisionLayer)) {
            // If collision is detected, adjust the camera's position
            desiredPosition = hit.point + hit.normal * collisionRadius; //hit.point is where the ray hit the object,
            //hit.normal is the normal vector of the surfact at point of contact, so it's used to push
            //the camera away from the surface by multipling it with collisionRadius
        }

        // Move the camera smoothly towards the desired position by using the .Lerp method
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }
}

