using System;
using System.Collections;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;
    int isWalkingForwardHash;
    int isWalkingBackwardHash;
    int isWalkingLeftHash;
    int isWalkingRightHash;
    bool isWalkingInAnyDiraction;
    public bool isGrounded;

    private void OnEnable()
    {
        playerController.OnGroundedChanged += UpdateGroundedState;
    }

    private void OnDisable()
    {
        playerController.OnGroundedChanged -= UpdateGroundedState;
    }

    private void UpdateGroundedState(bool _isGrounded)
    {
        isGrounded = _isGrounded;
        animator.SetBool("IsGrounded", _isGrounded);
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingForwardHash = Animator.StringToHash("isWalkingForward");
        isWalkingBackwardHash = Animator.StringToHash("isWalkingBackward");
        isWalkingLeftHash = Animator.StringToHash("isWalkingLeft");
        isWalkingRightHash = Animator.StringToHash("isWalkingRight");
    }

    void Update()
    {
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool backwardPressed = Input.GetKey(KeyCode.S);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.D);
        bool jumpPressed = Input.GetKeyDown(KeyCode.Space);

        bool isJumping = animator.GetBool("jumpFromIdle") || animator.GetBool("jumpFromWalking");

        isWalkingInAnyDiraction = animator.GetBool("isWalkingForward") || animator.GetBool("isWalkingBackward") || animator.GetBool("isWalkingLeft") || animator.GetBool("isWalkingRight");
        // Forward Movement
        animator.SetBool(isWalkingForwardHash, forwardPressed && !isJumping);

        // Backward Movement
        animator.SetBool(isWalkingBackwardHash, backwardPressed && !isJumping);

        // Left Movement
        animator.SetBool(isWalkingLeftHash, leftPressed && !isJumping);

        // Right Movement
        animator.SetBool(isWalkingRightHash, rightPressed && !isJumping);

        // Jump
        if (jumpPressed && !isJumping)
        {
            StartCoroutine(JumpOnce());
            print("JUMPING");
        }
    }

    IEnumerator JumpOnce()
    {
        //if (animator.GetBool("jumpFromIdle") || animator.GetBool("jumpFromWalking") || !isGrounded)
        //{
        //    yield return null;
        //}
        if (!isWalkingInAnyDiraction && isGrounded)
        {
            animator.SetBool("jumpFromIdle", true);
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + animator.GetCurrentAnimatorStateInfo(0).normalizedTime - 6);
            animator.SetBool("jumpFromIdle", false);
        }
        else if (isGrounded)
        {
            animator.SetBool("jumpFromWalking", true);
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + animator.GetCurrentAnimatorStateInfo(0).normalizedTime - 1.5f);
            animator.SetBool("jumpFromWalking", false);
        }
    }
}
