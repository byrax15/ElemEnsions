using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    [SerializeField] private AudioClip[] jumpClips;
    [SerializeField] private AudioSource audioSource;
    public Animator animator;
    private bool isRunning;
    private int isRunningHash;
    private int onFallHash;
    private bool onGround = true;
    private int onJumpHash;
    private int onLandHash;
    private bool isSprinting;
    private int isSprintingHash;




    private void Start()
    {
        isRunningHash = Animator.StringToHash("isRunning");
        onJumpHash = Animator.StringToHash("onJump");
        onFallHash = Animator.StringToHash("onFall");
        onLandHash = Animator.StringToHash("onLand");
        isSprintingHash = Animator.StringToHash("isSprinting");

    }

    private void Update()
    {
        isRunning = animator.GetBool(isRunningHash);
    }

    public void OnMove()
    {
        if (onGround)
            if (!isRunning)
                animator.SetBool(isRunningHash, true);
        
    }

    public void OnStop()
    {
        if (isRunning) animator.SetBool(isRunningHash, false);
    }

    public void OnFall()
    {
        onGround = false;
        animator.SetBool(onFallHash, true);
    }

    public void OnLand()
    {
        if (!onGround)
        {
            onGround = true;
            animator.SetTrigger(onLandHash);
            animator.SetBool(onFallHash, false);
        }
    }

    public void OnJump()
    {
        onGround = false;
        animator.SetTrigger(onJumpHash);
        animator.SetBool(onFallHash, true);

        audioSource.PlayOneShot(jumpClips[(int)Random.Range(0, (float)jumpClips.Length)], 0.7f);
    }

    public void OnSprint()
    {
        if (onGround)
            if (!isSprinting)
                animator.SetBool(isSprintingHash, true);
    }

    public void OnSprintStop()
    {
        animator.SetBool(isSprintingHash, false);
    }
}