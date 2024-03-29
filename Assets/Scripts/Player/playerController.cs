using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f; // Force applied when jumping
    public float hitForce = 5f; // Force applied when hit by a trap
    public float disableDuration = 3f; // Duration for which controls are disabled
    private bool isDisabled; // Flag to check if controls are disabled
    private float disableTimer; // Timer to keep track of disabled duration
    public bool isGrounded;

    public Rigidbody rb;

    public static event Action<bool> OnGroundedChanged; // Event for grounded state change

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cutsceneTimer = cutsceneDuration;
    }


    public float cutsceneDuration = 5f; // Duration of the cutscene in seconds
    private float cutsceneTimer;
    void Update()
    {
        if (cutsceneTimer > 0)
        {
            cutsceneTimer -= Time.deltaTime;
            if (cutsceneTimer <= 0)
            {

            }
        }
        else if (isDisabled)
        {
            disableTimer -= Time.deltaTime;
            if (disableTimer <= 0)
            {
                isDisabled = false; // Re enable controls
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
                Vector3 jumpVector = Vector3.up * jumpForce;
                if (z > 0)
                {
                    jumpVector += transform.forward * z;
                }

                rb.AddForce(jumpVector, ForceMode.Impulse);
            }
        }

    }
    public float groundCheckAngle = 45f; // Maximum angle to be considered as grounded

    // Check if the player is grounded
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            if (IsSurfaceFlat(other))
            {
                OnGroundedChanged?.Invoke(isGrounded);
                isGrounded = true;
            }
        }
        if (other.gameObject.CompareTag("Trap"))
        {
            Vector3 direction = transform.position - other.transform.position;
            direction.y = 0;

            rb.velocity = Vector3.zero; // Reset existing velocity
            rb.AddForce(Vector3.back * hitForce, ForceMode.Impulse);
            isDisabled = true; // Disable controls
            disableTimer = disableDuration; // Reset the timer
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            OnGroundedChanged?.Invoke(isGrounded);
        }
    }

    //Checking the angle to see if the floor is flat. 
    private bool IsSurfaceFlat(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            float angle = Vector3.Angle(Vector3.up, contact.normal);
            if (angle < groundCheckAngle)
            {
                return true;
            }
        }
        return false;
    }
}
