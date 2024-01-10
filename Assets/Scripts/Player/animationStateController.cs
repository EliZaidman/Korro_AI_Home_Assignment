using System;
using System.Collections;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;
    playerController _playerController;
    int isWalkingForwardHash;
    int isWalkingBackwardHash;
    int isWalkingLeftHash;
    int isWalkingRightHash;
    bool isWalkingInAnyDiraction;
    public bool isGrounded;

    private void OnEnable()
    {
        _playerController = GetComponent<playerController>();
        playerController.OnGroundedChanged += UpdateGroundedState;
        PlayerHealth.OnGettingHit += PlayerHealth_OnGettingHit;
    }

    private void OnDisable()
    {
        playerController.OnGroundedChanged -= UpdateGroundedState;
        PlayerHealth.OnGettingHit -= PlayerHealth_OnGettingHit;
    }

    private void PlayerHealth_OnGettingHit(int obj)
    {
        animator.Play("Hit", -1, 0); // Force play the animation
    }

    private void UpdateGroundedState(bool _isGrounded)
    {
        isGrounded = _isGrounded;
        animator.SetBool("isGrounded", _isGrounded);

        if (!_isGrounded)
            animator.SetBool("isJumping", false);
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

        isWalkingInAnyDiraction = animator.GetBool("isWalkingForward") || animator.GetBool("isWalkingBackward") || animator.GetBool("isWalkingLeft") || animator.GetBool("isWalkingRight");

        // Forward Movement
        animator.SetBool(isWalkingForwardHash, forwardPressed && _playerController.isGrounded);

        // Backward Movement
        animator.SetBool(isWalkingBackwardHash, backwardPressed && _playerController.isGrounded);

        // Left Movement
        animator.SetBool(isWalkingLeftHash, leftPressed && _playerController.isGrounded);

        // Right Movement
        animator.SetBool(isWalkingRightHash, rightPressed && _playerController.isGrounded);

        // Jump
        if (jumpPressed && _playerController.isGrounded)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            {
                return;
            }
            animator.Play("Jump", -1, 0); // Force play the animation
        }
    }
}
