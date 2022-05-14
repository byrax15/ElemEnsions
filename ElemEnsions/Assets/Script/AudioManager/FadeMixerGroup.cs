using System.Collections;
using UnityEngine.Audio;
using UnityEngine;

public static class FadeMixerGroup
{
    public static void SetVolume(this AudioMixer mixer, string exposedParam, float value)
    {
        Debug.Log("Volume");
        mixer.SetFloat(exposedParam, Mathf.Lerp(-80.0f, 0.0f, Mathf.Clamp01(value)));
    }

    public static IEnumerator StartFade(AudioMixer mixer, string exposedParam, float duration, float targetVolume)
    {
        float currentTime = 0;
        float currentVol;
        mixer.GetFloat(exposedParam, out currentVol);
        currentVol = Mathf.Pow(10, currentVol / 20);
        float targetValue = Mathf.Clamp(Mathf.Lerp(-80.0f, 0.0f, Mathf.Clamp01(targetVolume)), 1, 0.0001f);
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / duration);
            mixer.SetFloat(exposedParam, Mathf.Log10(newVol) * 20);
            yield return null;
        }
        yield break;
    }

    public static IEnumerator EndFade(AudioMixer mixer, string exposedParam, float duration)
    {
        float currentTime = 0;
        float currentVol;
        mixer.GetFloat(exposedParam, out currentVol);
        currentVol = Mathf.Pow(10, currentVol / 20);
        float targetValue = 0.0001f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / duration);
            Debug.Log(Mathf.Log10(newVol) * 20);
            mixer.SetFloat(exposedParam, Mathf.Log10(newVol) * 20);
            yield return null;
        }
        yield break;
    }
}
