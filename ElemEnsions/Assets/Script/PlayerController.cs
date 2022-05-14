using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController cr;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float jumpForce = 100.0f;
    [SerializeField] private float rotationSpeed = 0.8f;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.1f;
    [SerializeField] private LayerMask groundMask;


    private bool isGrounded = false;
    private bool isJumping = false;

    private Vector3 velocity;
    private Vector3 movement;

    private float speed;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // downward acceleration
        if (isGrounded && velocity.y < -2) //We only reset the velocity if we were falling and before we were grounded
        {
            velocity.y = -2f;
        }
        else if(isJumping)
        {
            velocity += Vector3.up * jumpForce;
            isJumping = false;
            isGrounded = false;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        Vector3 move = movement.x * cameraTransform.right.normalized + movement.z * cameraTransform.forward.normalized;
        move.y = 0.0f;
        cr.Move((velocity + move) * Time.deltaTime);

        Quaternion rotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    public void OnApplicationFocus(bool focus)
    {
        if(focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
            return;
        }
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 input = context.ReadValue<Vector2>();
        
            movement = speed * new Vector3(input.x, 0.0f, input.y).normalized;
        }
        if(context.canceled)
            movement = Vector3.zero;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            if(isGrounded)
            {
                isJumping = true;
            }
        }
    }
}
