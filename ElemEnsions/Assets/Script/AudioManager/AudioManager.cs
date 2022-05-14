using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer = null;
    private string currentAudio = "";



    private void Start()
    {
        Switch("Fire", "Wind");
    }

    private void Switch(string from, string to)
    {
        mixer.SetVolume("Fire", 1.0f);
        mixer.SetVolume("Wind", 0.0f);
        StartCoroutine(FadeMixerGroup.EndFade(mixer, "Fire", 10.0f));
        StartCoroutine(FadeMixerGroup.StartFade(mixer, "Wind", 10.0f, 1f));
    }

    public void SetUniqueAudio(string dimension)
    {

    }
}
