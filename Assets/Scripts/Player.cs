using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

[RequireComponent(typeof(CharacterController2D))]
public class Player : MonoBehaviour
{
    public float gravity = -10f, movementSpeed = 10f, jumpHeight = 5f, centreRadius = .5f;

    private CharacterController2D controller;
    private Animator anim;
    private SpriteRenderer rend;
    private Vector3 velocity;
    private bool isClimbing = false;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, centreRadius);
    }

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
        if (!controller.isGrounded && !isClimbing)
        {
            velocity.y += gravity * Time.deltaTime; //apply gravity
        }
        if (controller.isGrounded)
        {
            Jump();
        }
        Move(inputH);
        Climb(inputV, inputH);
        Run();
        Crouch();

        if (!isClimbing)
        {
            controller.move(velocity * Time.deltaTime); //apply movement with motion
        }
    }

    void Move(float inputH)
    {
        velocity.x = inputH * movementSpeed;//move left and right
        rend.flipX = inputH < 0;
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = jumpHeight;
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

    void Climb(float inputV, float inputH)
    {
        bool isOverLadder = false;

        #region Detecting Ladders
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, centreRadius); // Get a list of all hit objects overlapping point        
        foreach (var hit in hits) // Loop through all hit objects
        {
            if (hit.tag == "Ground")
            {
                isClimbing = false;
                isOverLadder = false;
            }

            if (hit.tag == "Ladder") //Check if tagged "Ladder"
            {
                isOverLadder = true; //Player is overlapping a Ladder!
                break; //exit just the foreach loop
            }
        }
        if (isOverLadder && inputV != 0) // If the player is overlapping AND input vertical is made
        {
            isClimbing = true; //The player is in Climbing state!
        }
        #endregion

        #region Translating the Player        
        if (isClimbing) // If player is climbing
        {
            velocity.y = 0;
            Vector3 inputDir = new Vector3(inputH, inputV, 0);
            transform.Translate(inputDir* movementSpeed * Time.deltaTime); //Move player up and down on the ladder (additionally move left and right)
        }

        if (!isOverLadder)
        {
            isClimbing = false;
        }
        #endregion

        anim.SetBool("isClimbing", isClimbing);
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
