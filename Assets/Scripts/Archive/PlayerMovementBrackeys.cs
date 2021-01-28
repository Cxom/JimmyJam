using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementBrackeys : MonoBehaviour
{
    [SerializeField] private string movementForwardAxis = "Vertical";
    [SerializeField] private string movementSideAxis = "Horizontal";

    [SerializeField] private CharacterController controller;
    
    [SerializeField] private float movementSpeed = 12f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 3.0f;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundLayer;
    
    private Vector3 velocity;
    private bool isGrounded;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        CalculateIsGrounded();

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        } 
        
        float forwardInput = Input.GetAxis(movementForwardAxis);
        float sideInput = Input.GetAxis(movementSideAxis);

        Vector3 moveInput = transform.forward * forwardInput + transform.right * sideInput;

        controller.Move(moveInput * movementSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime );
    }

    private void CalculateIsGrounded()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);
    }
}
