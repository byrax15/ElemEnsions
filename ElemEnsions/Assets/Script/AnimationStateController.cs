using Script;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    [SerializeField] private AudioClip[] jumpClips;
    [SerializeField] private AudioClip[] stepBaseClip;
    [SerializeField] private AudioClip[] stepFireClip;
    [SerializeField] private AudioClip[] stepAirClip;
    [SerializeField] private AudioClip[] stepWaterClip;
    [SerializeField] private AudioClip[] stepEarthClip;

    Dimension currDim = Dimension.Base;
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
    [SerializeField] private DimensionChangeMediator mediator;


    private void Start()
    {
        isRunningHash = Animator.StringToHash("isRunning");
        onJumpHash = Animator.StringToHash("onJump");
        onFallHash = Animator.StringToHash("onFall");
        onLandHash = Animator.StringToHash("onLand");
        isSprintingHash = Animator.StringToHash("isSprinting");

        mediator.AddListener(SwitchDimension);

    }

    private void SwitchDimension(Dimension arg0, Dimension newDim)
    {
        currDim = newDim;
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

    public void FootstepSound()
    {
        switch(currDim)
        {
            case Dimension.Base:
                audioSource.PlayOneShot(stepBaseClip[(int)Random.Range(0, (float)stepBaseClip.Length)], 0.3f);
                break;
            case Dimension.Earth:
                audioSource.PlayOneShot(stepEarthClip[(int)Random.Range(0, (float)stepEarthClip.Length)], 0.3f);
                break;
            case Dimension.Fire:
                audioSource.PlayOneShot(stepFireClip[(int)Random.Range(0, (float)stepFireClip.Length)], 0.3f);
                break;
            case Dimension.Water:
                audioSource.PlayOneShot(stepWaterClip[(int)Random.Range(0, (float)stepWaterClip.Length)], 0.3f);
                break;
            case Dimension.Air:
                audioSource.PlayOneShot(stepAirClip[(int)Random.Range(0, (float)stepAirClip.Length)], 0.3f);
                break;
        }
    }
    public void SprintSound()
    {
        FootstepSound();
    }
}
