using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using Script;
using System;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer = null;

    [SerializeField] private DimensionChangeMediator mediator;
    private Dimension current;

    public int count;

    public void SwitchDimension(Dimension oldDim, Dimension newDim)
    {
        current = newDim;
        StartCoroutine(FadeMixerGroup.EndFade(mixer, oldDim.ToString(), 10.0f));
        StartCoroutine(FadeMixerGroup.StartFade(mixer, newDim.ToString(), 10.0f, 1f));
    }

    private void Start()
    {
        var dimensions = Enum.GetValues(typeof(Dimension));
        foreach (Dimension dim in dimensions)
        {
            if (dim == mediator.CurrentDimension)
            {
                mixer.SetVolume(dim.ToString(), 1.0f);
                continue;
            }

            mixer.SetVolume(dim.ToString(), 0.0f);
        }
        mediator.AddListener(SwitchDimension);
    }

    public void TestForceChangeDimension(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) 
        {
            Dimension d = ((Dimension[])Enum.GetValues(typeof(Dimension)))[count++ % 6];  
            
            mediator.ChangeDimension(d);
        }
        
    }
}
