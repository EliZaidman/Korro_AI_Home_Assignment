using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;
    int isWalkingForwardHash;
    int isWalkingBackwardHash;
    int isWalkingLeftHash;
    int isWalkingRightHash;

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
        bool jumpPressed = Input.GetKey(KeyCode.Space);

        bool isJumping = animator.GetBool("jumpFromIdle") || animator.GetBool("jumpFromWalking");

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
        }
    }

    IEnumerator JumpOnce()
    {
        if (animator.GetBool("isWalking") == true)
        {
            animator.SetBool("jumpFromWalking", true);
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + animator.GetCurrentAnimatorStateInfo(0).normalizedTime - 0.5f);
            animator.SetBool("jumpFromWalking", false);
        }
        else
        {
            animator.SetBool("jumpFromIdle", true);
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + animator.GetCurrentAnimatorStateInfo(0).normalizedTime - 1.5f);
            animator.SetBool("jumpFromIdle", false);
        }
    }
}
