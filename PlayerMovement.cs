using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float playerHeight = 2f;

    [SerializeField] Transform orientation;

    public float jumpForce = 5f;

    public float moveSpeed = 6f;
    public float sprintSpeed = 10f;
    public float sneakSpeed = 4f;
    float movementMultiplier = 10f;
    [SerializeField] float airMultiplier = 0.4f; //variables for the speed of the player in specific scenarios

    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] KeyCode sneakKey = KeyCode.LeftControl; //keycodes that can be changed int he inspector
    
    float groundDrag = 6f;
    float airDrag = 2f; //variable sto control the drag in different states

    float horizontalMovement; //instantiates a float for horizontal movement
    float verticalMovement; //instantiates a float for vertical movement
    
    bool isGrounded; //makes a bool to check if the player is on the ground

    Vector3 moveDirection; //instantiates a vecotor3

    Rigidbody rb; //instantiates a rigidbody

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); //sets the rigidbody to the players rigidbody
        rb.freezeRotation = true;
    }

    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight / 2 + 0.1f); //sends a raycast down from the middle of the player 

        myInput(); //calls the function myInput
        controlDrag(); //calls the function controlDrag

        
        if (Input.GetKeyDown(jumpKey) && isGrounded) //checks if the jumpKeys is pressed and the isgrounded bool is true
        {
            Jump(); //calls the jump function
        }
        if (Input.GetKeyDown(jumpKey))
        {
            print("space pressed");
        }
    }

    void myInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal"); // gets the x-axis input from Unity called horizontal
        verticalMovement = Input.GetAxisRaw("Vertical"); // gets the y-axis input from Unity called vertical

        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement; //sets the vecotor3 values for the x and y in the variable
    }

    void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse); //Impulse so it adds sudden force upwards
    }

    void controlDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag; //sets the drag of the player to ground drag when touching items with the layer Ground
        }
        else
        {
            rb.drag = airDrag; //sets the drag of the player to air drag when it is not touching the ground
        }
    }

    private void FixedUpdate()
    {
        movePlayer();
    }

    void movePlayer()
    {
        if (isGrounded)
        {
            if (Input.GetKey(sprintKey))
            {
                print("shift pressed");
                rb.AddForce(moveDirection.normalized * sprintSpeed * movementMultiplier, ForceMode.Acceleration);//speed of movement when left shift is held down
            }
            else if (Input.GetKey(sneakKey))
            {
                rb.AddForce(moveDirection.normalized * sneakSpeed * movementMultiplier, ForceMode.Acceleration);//speed of movement when left control is held down
            }
            else
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);//speed of movement when on the ground
            }
        } 
        else if (!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);//speed of movement when on the ground.
        }
    }
}
