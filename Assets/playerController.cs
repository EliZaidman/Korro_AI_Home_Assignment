using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public Rigidbody rb;
    public bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
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

    // Check if the player is grounded
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

}
