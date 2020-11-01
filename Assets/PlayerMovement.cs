using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement; //So you can use SceneManager
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;

    public float runSpeed = 60f;

    bool jump = false;
    bool jumpdelayactive = false;
    float horizontalMove = 0f;
    public GameDataHandler mytools;

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0) //don't allow movement if paused
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
            animator.SetFloat("speed", Mathf.Abs(horizontalMove));
            
            if (Input.GetButtonDown("Jump") && !jumpdelayactive)
            {
                jump = true;
                animator.SetBool("isjumping", true);
                jumpdelayactive = true;
                Invoke("EndJumpDelay", 0.3f);
            }
        }
    }

    public void EndJumpDelay()
    {
        jumpdelayactive = false;
        //Debug.Log("jumpdelay ended");
    }

    public void OnLanding()
    {
        if (!jumpdelayactive)
        {
            animator.SetBool("isjumping", false);
            //Debug.Log("Player Landed");
        }
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump); //(hz move, crouch, jump)
        jump = false;
    }
}
