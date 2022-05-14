using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController cr;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpForce;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.1f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private ParticleSystem ps;


    private bool isGrounded = false;
    private bool isJumping = false;
    private bool canDoubleJump = true;

    private Vector3 verticalVelocity;
    private Vector3 movement;

    [SerializeField] private float speed;

    // Update is called once per frame
    void Update()
    {
        if (isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask))
            canDoubleJump = true;

        // downward acceleration
        if (isGrounded && verticalVelocity.y < -2) //We only reset the velocity if we were falling and before we were grounded
        {
            verticalVelocity.y = -2f;
        }
        else if(isJumping)
        {
            if(!isGrounded)
                canDoubleJump = false;
            verticalVelocity += Vector3.up * jumpForce;
            if (verticalVelocity.magnitude > jumpForce)
                verticalVelocity.y = jumpForce;
            isJumping = false;
            isGrounded = false;
        }
        else
        {
            verticalVelocity.y += gravity * Time.deltaTime;
        }

        Vector3 move = movement.x * cameraTransform.right.normalized + movement.z * cameraTransform.forward.normalized;
        move.y = 0.0f;
        cr.Move((verticalVelocity + move) * Time.deltaTime);

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
        isJumping = context.performed && (isGrounded || canDoubleJump);
        if(isJumping)
        {
            ps.Emit(100);
        }
    }
}
