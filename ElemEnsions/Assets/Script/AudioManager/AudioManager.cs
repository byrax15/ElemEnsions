using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using Script;
using System;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer = null;

    [SerializeField] private DimensionChangeMediator mediator;

    [SerializeField] private float maxMusicVolume = 0.8f;

    private void Start()
    {
        var dimensions = Enum.GetValues(typeof(Dimension));
        foreach (Dimension dim in dimensions)
        {
            if (dim == mediator.CurrentDimension)
            {
                mixer.SetVolume(dim.ToString(), maxMusicVolume);
                continue;
            }

            mixer.SetVolume(dim.ToString(), 0.0f);
        }

        mediator.AddListener(SwitchDimension);
    }

    public void SwitchDimension(Dimension oldDim, Dimension newDim)
    {
        StartCoroutine(FadeMixerGroup.EndFade(mixer, oldDim.ToString(), 5.0f));
        StartCoroutine(FadeMixerGroup.StartFade(mixer, newDim.ToString(), 5.0f, maxMusicVolume));
    }
}