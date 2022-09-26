using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerController controller;
    public float runSpeed = 40f;

    public Animator player_animator;

    private float horizontalMove = 0f;
    private bool jumping = false;


    public float jumpTime = 0.2f;
	private float jumpTimeCount;
    private bool bufferJumpFalse = false;

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        player_animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump") && controller.isGrounded())
        {
            jumping = true;
            jumpTimeCount = jumpTime;
            player_animator.SetBool("Jump", true);
        }

        if (Input.GetKey(KeyCode.Space) && jumping == true)
        {
            if (jumpTimeCount > 0)
            {
                jumping = true;
            }
            else
            {
                jumping = false;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            if (jumpTimeCount > 0.075f)
            {
                bufferJumpFalse = true;
            }
            else
            {
                jumping = false;
            }
        }

        if (bufferJumpFalse == true)
        {
            if (jumpTimeCount < 0.075f)
            {
                jumping = false;
                bufferJumpFalse = false;
            }
        }

        if (jumpTimeCount > 0)
            {
                jumpTimeCount -= Time.deltaTime;
            }

        if (jumping == false && controller.isGrounded() == true)
        {
            player_animator.SetBool("Jump", false);
        }
    }


    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, jumping);
    }
}
