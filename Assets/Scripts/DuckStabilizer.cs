using UnityEngine;

public class DuckStabilizer : MonoBehaviour {
    public float stabilityStrength = 10f; // How strongly to keep the duck upright
    public float maxTiltAngle = 45f; // Maximum angle of tilt(45 degrees) before trying to correct it
    public float rotationSpeed = 5f; // Speed at which the duck rotates back upright
    public Transform cameraTransform; // The camera the duck should face
    public float inputRotationSpeed = 10f; // Speed at which the duck rotates to left/right based on input

    private Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        // Get the current local rotation on the X and Z axes
        Vector3 currentRotation = transform.rotation.eulerAngles;
        float tiltX = Mathf.Abs(currentRotation.x);
        float tiltZ = Mathf.Abs(currentRotation.z);

        // Check if the duck is tilted beyond the threshold (X or Z axis)
        if (tiltX > maxTiltAngle || tiltZ > maxTiltAngle) {
            // Apply a corrective force to return to an upright position
            if (tiltX > maxTiltAngle) {
                // Rotate the duck upright along the X axis
                Quaternion targetRotation = Quaternion.Euler(0, currentRotation.y, currentRotation.z);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }

            if (tiltZ > maxTiltAngle) {
                // Rotate the duck upright along the Z axis
                Quaternion targetRotation = Quaternion.Euler(currentRotation.x, currentRotation.y, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
        }

        // Handle input for duck looking left (A), left(A) and forward(W), right(D) and forward(W) or right (D)
        if (cameraTransform != null) {
            // Rotate the duck to the left by 45 degrees
            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W)) {
                Quaternion targetRotation = Quaternion.Euler(0, -45, 0); // Adjust the rotation as needed
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * inputRotationSpeed);
            }
            // Rotate the duck to the right by 45 degrees
            else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W)) {
                Quaternion targetRotation = Quaternion.Euler(0, 45, 0); // Adjust the rotation as needed
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * inputRotationSpeed);
            }
            else if (Input.GetKey(KeyCode.A)) {
                // Rotate the duck to the left by 90 degrees
                Quaternion targetRotation = Quaternion.Euler(0, -90, 0); // Adjust the rotation as needed
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * inputRotationSpeed);
            }
            else if (Input.GetKey(KeyCode.D)) {
                // Rotate the duck to the right by 90 degrees
                Quaternion targetRotation = Quaternion.Euler(0, 90, 0); // Adjust the rotation as needed
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * inputRotationSpeed);
            }
            else {
                // If no key is pressed, keep the duck facing forward
                Vector3 directionToCamera = transform.position - cameraTransform.position;  // Reversed direction to face away
                directionToCamera.y = 0;  // Keep the duck from tilting up or down
                Quaternion targetRotation = Quaternion.LookRotation(directionToCamera);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
        }
    }
}
