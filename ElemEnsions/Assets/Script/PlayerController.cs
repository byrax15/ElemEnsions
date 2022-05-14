using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public GameObject HeldItem;

    [SerializeField] private CharacterController cr;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpForce;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.5f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private Transform WallJumpCheck;
    
    [SerializeField] private InteractableManager _interactableManager;
    
    private bool isGrounded;
    private bool isJumping;
    private bool canDoubleJump = true;
    private bool isTouchingWall;
    private bool canWallJump = true;


    private bool canUpdateDoubleJump = true;

    private Vector3 verticalVelocity;
    private Vector3 movement;
    private Vector3 WallJumpCheckPositionLow;

    [SerializeField] private float speed;

    private int crystals = 0;
    public int Crystals { 
        get => crystals;

        set 
        {
            crystals = value;
            GetComponent<PlayerUI>().UpdateCrystalsCount(crystals);
        }
    }

    void Update()
    {
        if(canWallJump)
        {
            isGrounded = Physics.CheckCapsule(groundCheck.position, WallJumpCheck.position, 0.51f, groundMask);
            isTouchingWall = isGrounded;
        }
        else
        {
            if (isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask))
                canDoubleJump = true;
        }


        // downward acceleration
        if (isGrounded && verticalVelocity.y < -2) //We only reset the velocity if we were falling and before we were grounded
        {
            verticalVelocity.y = -2f;
        }
        else if(isJumping)
        {
            if(!isGrounded)
                CheckUpdateCanDoubleJump(false);
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

    public void CheckUpdateCanDoubleJump(bool newValue)
    {
        if(canUpdateDoubleJump)
            canDoubleJump = newValue;
    }

    public void CheckWallJump()
    {

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
        if(isTouchingWall && canWallJump)
        {
            CheckWallJump();
        }

        isJumping = context.performed && (isGrounded || canDoubleJump);
        if(isJumping)
        {
            ps.Emit(100);
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
                _interactableManager.DoCurrentInteraction();
    }

    public void OnDrop(InputAction.CallbackContext context)
    {
        if (context.performed && HeldItem != null)
        {
            HeldItem.transform.parent = null;
            HeldItem = null;
        }
    }
}
