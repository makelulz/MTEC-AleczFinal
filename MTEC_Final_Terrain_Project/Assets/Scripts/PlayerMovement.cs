using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    // The jumpSpeed is simply the amount of force applied to the jump when Space is pressed
    public float jumpSpeed = 6f;
    //This variable is how far out the Raycast goes. It is the same distance as our cube is from its center point to its bottom edge.
    public float distFromGround = 1;

    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -14.715f;

    Vector3 velocity;

    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (isGrounded() && velocity.y < 0)
        {
            velocity.y = -2f;
        } 

        // This if statement uses the prebuilt Unity Jump action and works in conjunction with the Raycast to now allow us to jump if we are already airborne.
       if (Input.GetButtonDown("Jump") && isGrounded())
        {
            
            velocity.y = Mathf.Sqrt(jumpSpeed * -2f * gravity);
        }

        if (velocity.y < 0)
        {
            //This is a simple way of saying, if the object is falling, use this If statement to multiply it by the fall speed we want.
            //The video explained that the -1 is to account for the built in gravity unity is already using. It rounds the -0.9 and change to 1 for simplicity.
            velocity.y += gravity * (fallMultiplier - 1.5f) * Time.deltaTime;
        }

        //This is saying if the object is gaining velocity and we are not holding the jump button anymore, impose a different level of velocity
        else if (velocity.y > 0 && !Input.GetButton("Jump"))
        {

            velocity.y = 0f;
            velocity.y +=  gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }


    }
    // This simple bool function is checking the raycast, which we can then use the output of in the previous if statement
    bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distFromGround);
    }
}
