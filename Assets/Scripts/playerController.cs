using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float hitForce = 5f; // Force applied when hit by a trap
    public float disableDuration = 3f; // Duration for which controls are disabled
    private bool isDisabled; // Flag to check if controls are disabled
    private float disableTimer; // Timer to keep track of disabled duration
    public bool isGrounded;

    public Rigidbody rb;

    public static event Action<bool> OnKeyReleased; // Event for grounded state change
    public static event Action<bool> OnGroundedChanged; // Event for grounded state change

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isDisabled)
        {
            disableTimer -= Time.deltaTime;
            if (disableTimer <= 0)
            {
                isDisabled = false; // Re-enable controls
            }
        }
        else
        {
            // Movement input
            float x = Input.GetAxis("Horizontal") * moveSpeed;
            float z = Input.GetAxis("Vertical") * moveSpeed;

            // Apply movement
            Vector3 move = transform.right * x + transform.forward * z;
            rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);

            // Jump input
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                // Add forward momentum to the jump if moving forward
                Vector3 jumpVector = Vector3.up * jumpForce;
                if (z > 0)
                {
                    jumpVector += transform.forward * z;
                }

                rb.AddForce(jumpVector, ForceMode.Impulse);
            }
        }

    }

    // Check if the player is grounded
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            OnGroundedChanged?.Invoke(isGrounded); // Invoke the event
        }
        if (other.gameObject.CompareTag("Trap"))
        {
            Vector3 hitDirection = (transform.position - other.transform.position).normalized;
            rb.AddForce(hitDirection * hitForce, ForceMode.Impulse);
            isDisabled = true; // Disable controls
            disableTimer = disableDuration; // Reset the timer
            print("OUTCH ITS TRAP");
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            OnGroundedChanged?.Invoke(isGrounded); // Invoke the event
        }
    }

}
