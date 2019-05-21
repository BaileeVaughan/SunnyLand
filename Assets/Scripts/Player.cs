using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

[RequireComponent(typeof(CharacterController2D))]
public class Player : MonoBehaviour
{
    public float gravity = -10f;
    public float movementSpeed = 10f;
    public CharacterController2D controller;

    private Vector3 motion;

    void Reset()
    {
        controller = GetComponent<CharacterController2D>();
    }

    void Update()
    {
        float inputH = Input.GetAxis("Horizontal"); //get horizontal input

        motion.x = inputH * movementSpeed;//move left and right

        if (controller.isGrounded)
        {
            motion.y = 0f;
        }

        motion.y += gravity * Time.deltaTime; //apply gravity

        controller.move(motion * Time.deltaTime); //apply movement with motion
    }
}
