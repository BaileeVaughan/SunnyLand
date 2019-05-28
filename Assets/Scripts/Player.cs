using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

[RequireComponent(typeof(CharacterController2D))]
public class Player : MonoBehaviour
{
    public float gravity = -10f, movementSpeed = 10f, jumpHeight = 5f;

    private CharacterController2D controller;
    private Animator anim;
    private SpriteRenderer rend;
    private Vector3 motion;

    private void Start()
    {
        controller = GetComponent<CharacterController2D>();
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();       
    }

    void Reset()
    {
        controller = GetComponent<CharacterController2D>();
    }

    void Update()
    {
        float inputV = Input.GetAxis("Vertical"); //get vertical input
        float inputH = Input.GetAxis("Horizontal"); //get horizontal input
        if (!controller.isGrounded)
        {
            motion.y += gravity * Time.deltaTime; //apply gravity
        }
        if (controller.isGrounded)
        {
            Jump();
        }
        Move(inputH);
        Run();           
        Crouch();
    }

    void Move(float inputH)
    {
        motion.x = inputH * movementSpeed;//move left and right
        controller.move(motion * Time.deltaTime); //apply movement with motion
        rend.flipX = inputH < 0;
    }

    void Jump()
    {        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            motion.y = jumpHeight;
            anim.SetBool("isJumping", true);
        }
        else
        {            
            anim.SetBool("isJumping", false);
        }
    }

    void Run()
    {
        float inputH = Input.GetAxis("Horizontal"); //get horizontal input
        if (inputH > 0 || inputH < 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }

    void Climb()
    {
        anim.SetBool("isClimbing", true);
    }

    void Crouch()
    {
        if (Input.GetKey(KeyCode.C))
        {
            anim.SetBool("isCrouching", true);
        }
        else
        {
            anim.SetBool("isCrouching", false);
        }
    }

    void Hurt()
    {

        anim.SetBool("isHurt", true);
    }

}
