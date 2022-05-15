using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    public Animator animator;
    private bool isRunning;
    private int isRunningHash;
    private int onFallHash;
    private bool onGround = true;
    private int onJumpHash;
    private int onLandHash;

    private void Start()
    {
        isRunningHash = Animator.StringToHash("isRunning");
        onJumpHash = Animator.StringToHash("onJump");
        onFallHash = Animator.StringToHash("onFall");
        onLandHash = Animator.StringToHash("onLand");
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
    }
}