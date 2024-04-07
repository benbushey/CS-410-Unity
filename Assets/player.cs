using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

public class player : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    public int speed = 5;
    public float jumpForce = 5f; // Adjust the jump force as needed
    private int jumpCount = 0; // To track the number of jumps (for double jump)
    public int maxJump = 2; // Maximum number of jumps (1 for normal jump, 2 for double jump)

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void OnJump(InputValue value)
    {
        if (jumpCount < maxJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCount++;
        }
    }


    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    // Reset jump count when colliding with the ground
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].normal == Vector3.up) // Simple ground check
        {
            jumpCount = 0;
        }
    }
}
