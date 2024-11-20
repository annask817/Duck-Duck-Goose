using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public float speed = 6f;

    public DucklingManager dm;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Able to use WASD
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        //For help with direction
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        //If the player is pressing a movement key
        if (direction.magnitude >= 0.1f)
        {
            //Calucates the rotation angle so that our character is always facing the cameras direction
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            //To smooth out when the character turns
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //Moves the character
            controller.Move(direction * speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Duckling") && dm != null)
        {
            Debug.Log("Collecting duckling");
            Destroy(other.gameObject);
            dm.ducklingCount++;
            Debug.Log($"Duckling count: {dm.ducklingCount}");
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Log collisions for debugging
        Debug.Log($"Collided with: {hit.gameObject.name} with tag: {hit.gameObject.tag}");
    }
}