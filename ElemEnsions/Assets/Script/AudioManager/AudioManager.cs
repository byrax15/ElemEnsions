using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using Script;
using System;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer = null;

    [SerializeField] private DimensionChangeMediator mediator;

    [SerializeField] private float maxMusicVolume = 0.8f;

    [SerializeField] private AudioClip portalClip;

    [SerializeField] private AudioSource audioSource;

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
        StartCoroutine(PortalSound());
        StartCoroutine(FadeMixerGroup.EndFade(mixer, oldDim.ToString(), 3.0f));
        
        StartCoroutine(FadeMixerGroup.StartFade(mixer, newDim.ToString(), 3.0f, maxMusicVolume));
    }

    private IEnumerator PortalSound()
    {
        yield return new WaitForSeconds(0.2f);
        audioSource.PlayOneShot(portalClip, 0.7f);
    }
}