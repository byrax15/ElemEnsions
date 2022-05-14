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
    [SerializeField] private float groundDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private Transform WallJumpCheck;
    [SerializeField] private CheckWallJump CWJ;


    private bool isGrounded = false;
    private bool isJumpValid = false;
    private bool canDoubleJump = true;
    private bool isTouchingWall = false;
    private bool canWallJump = true;
    private Transform lastWallJumped;


    private bool canUpdateDoubleJump = true; // TODO update with dimension

    private Vector3 velocity;
    private Vector3 movement;

    private Transform WallCollided;

    [SerializeField] private float speed;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask) && velocity.y < 0)
        {
            CheckUpdateCanDoubleJump(true);
            lastWallJumped = null;
        }
        else if(canWallJump)
        {
            WallCollided = CWJ.GetWall();
            isTouchingWall = WallCollided != null;
        }


        // downward acceleration
        if(isJumpValid)
        {
            isJumpValid = false;
        }
        else if (isGrounded || isTouchingWall && velocity.y < -2) //We only reset the velocity if we were falling and before we were grounded
        {
            velocity.y = -2f;
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

    public void CheckUpdateCanDoubleJump(bool newValue)
    {
        if(canUpdateDoubleJump)
            canDoubleJump = newValue;
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
        bool wallJump = canWallJump && isTouchingWall && !isGrounded;
      //  isJumping = context.performed && (isGrounded || canDoubleJump || wallJump);
        if(context.performed && (isGrounded || canDoubleJump || wallJump))
        {
            isJumpValid = true;
            if (!isGrounded)
                CheckUpdateCanDoubleJump(false);
            if (wallJump)
            {
                if (lastWallJumped != WallCollided)
                    lastWallJumped = WallCollided;
                else
                    isJumpValid = false;
            }

            if (isJumpValid)
            {
                velocity += Vector3.up * jumpForce;
                if (velocity.magnitude > jumpForce)
                    velocity.y = jumpForce;
                ps.Emit(100);
            }
        }
    }
}
