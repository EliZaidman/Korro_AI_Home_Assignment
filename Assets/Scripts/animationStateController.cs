using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;

    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
    }

    // Update is called once per frame
    void Update()
    {
        bool forwordPressed = Input.GetKey(KeyCode.W);
        bool jumpPressed = Input.GetKey(KeyCode.Space);
        bool isWalking = animator.GetBool(isWalkingHash);

        #region Jumping
        bool isJumping;

        if (animator.GetBool("jumpFromIdle") == false || animator.GetBool("jumpFromWalking") == false)
            isJumping = false;
        else isJumping = true;
        #endregion


        if (forwordPressed && !isWalking && !isJumping)
        {
            animator.SetBool(isWalkingHash, true);
        }
        if(!forwordPressed && isWalking && !isJumping)
        {
            animator.SetBool(isWalkingHash, false);
        }
        if(jumpPressed && !isJumping) 
        {
            StartCoroutine(JumpOnce());
        }
    }

    IEnumerator JumpOnce()
    {
        if (animator.GetBool("isWalking") == true)
        {
            animator.SetBool("jumpFromWalking", true);
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            animator.SetBool("jumpFromWalking", false);
        }
        else
        {
            animator.SetBool("jumpFromIdle", true);
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            animator.SetBool("jumpFromIdle", false);
        }
    }
}
