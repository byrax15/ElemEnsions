using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController cr;
    public float speed;

    public float gravity = -9.8f;
    public float jumpForce = 100.0f;
    public float rotationSpeed = 0.8f;

    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public LayerMask groundMask;

    private bool isGrounded = false;
    private bool isJumping = false;

    private Vector3 velocity;
    private Vector3 movement;

    [SerializeField] private Transform cameraTransform;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // downward acceleration
        if (isGrounded &&
            velocity.y < -2) //We only reset the velocity if we were falling and before we were grounded
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
        }
        else
        {
            Cursor.lockState= CursorLockMode.None;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 input = context.ReadValue<Vector2>();
        
            movement = speed * new Vector3(input.x, 0.0f, input.y).normalized;
            Debug.Log(movement);
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
