using Script;
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
    [SerializeField] private ParticleSystem runPs;

    [SerializeField] private Transform WallJumpCheck;
    [SerializeField] private CheckWallJump CWJ;
    [SerializeField] private InteractableManager _interactableManager;

    private AnimationStateController ASC;
    private PlayerRespawn pR;
    
    private bool isGrounded;
    private bool canDoubleJump = false;
    private bool canWallJump = false;
    private bool canRun = false;
    private bool canUpdateDoubleJump = false;


    private bool isTouchingWall = false;

    private Transform lastWallJumped;



    private Vector3 velocity;
    private Vector3 movement;
    private Transform WallCollided;
    
    private float speed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintSpeed;

    private bool isJumpValid;

    private void Start()
    {
        ASC = GetComponent<AnimationStateController>();
        speed = walkSpeed;
        pR = GetComponent<PlayerRespawn>();
        pR.SetRespawnPoint(transform.position);        
    }

    private void Update()
    {
        bool lastIsGrounded = isGrounded;
        if (isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask) && velocity.y < 0) //pls keep it that way else the jump break
        {
            if (lastIsGrounded != isGrounded)
                ASC.OnLand();
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
        else if ((isGrounded || isTouchingWall || canUpdateDoubleJump) && velocity.y < -2 ) //We only reset the velocity if we were falling and before we were grounded
        {
            velocity.y = -2f;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        Vector3 move = movement.x * cameraTransform.right.normalized + movement.z * cameraTransform.forward.normalized;
        move.y = 0.0f;
        if (move.magnitude > 0.0f)
            ASC.OnMove();
        cr.Move((velocity + speed * move) * Time.deltaTime);

        if(velocity.y < 0 && !isGrounded)
        {
            ASC.OnFall();
        }

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
        
            movement = new Vector3(input.x, 0.0f, input.y).normalized;
            ASC.OnMove();
        }
        if(context.canceled)
        {
            movement = Vector3.zero;
            ASC.OnStop();
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        bool wallJump = canWallJump && isTouchingWall && !isGrounded;
        
        if(context.performed && (isGrounded || canDoubleJump || wallJump))
        {
            isJumpValid = true;
            if (!isGrounded && !wallJump) //Double jump

            {
                if (!canDoubleJump)
                {
                    isJumpValid = false;
                }
                else
                {
                    CheckUpdateCanDoubleJump(false);
                }
            }
            if (wallJump)//wallJump
            {

                if (lastWallJumped != WallCollided)
                    lastWallJumped = WallCollided;
                else
                    isJumpValid = false;
            }

            if (isJumpValid)
            {
                velocity += Vector3.up * jumpForce;
                if (velocity.y > jumpForce)
                    velocity.y = jumpForce;
                ASC.OnJump();
                ps.Emit(100);
            }
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if(canRun)
        {
            if (context.performed)
            {
                speed = sprintSpeed;
                runPs.Play();
            }
            else
            {
                StopRun();
            }
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
    public void StopRun()
    {
        speed = walkSpeed;
        runPs.Stop();
    }


    public void UpdatePowers(Dimension newDimension)
    {
        canUpdateDoubleJump = newDimension == Dimension.Air;
        canRun = newDimension == Dimension.Fire;
        canWallJump = newDimension == Dimension.Earth;
        if(isTouchingWall)
            isTouchingWall = newDimension == Dimension.Earth;
        if (canDoubleJump)
            canDoubleJump = newDimension == Dimension.Air;
        if (newDimension != Dimension.Fire)
            StopRun();

    }
}
