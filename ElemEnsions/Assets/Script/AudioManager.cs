using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Playables;

public class AudioManager : PlayableBehaviour
{
    private Playable mixer;
    private int inputCount;
    private int fadeIndex = 0;
    private float fadeDuration = 0.0f;

    public int CurrentIndex { get => fadeIndex; }

    public float FadeDuration { get => fadeDuration; }

    public static ScriptPlayable<AudioManager> Create(PlayableGraph graph, AudioMixerPlayable inputMixer) 
    {
        var result = ScriptPlayable<AudioManager>.Create(graph, 1);
        result.ConnectInput(0, inputMixer, 0, 1.0f);
        result.SetPropagateSetTime(true);
        
        var behaviour = result.GetBehaviour();
        behaviour.mixer = inputMixer;
        behaviour.inputCount = inputMixer.GetInputCount();

        return result;
    }

    public void Crossfade(int inputIndex, float duration)
    {
        if (duration == 0.0f) 
        {
            mixer.GetInput(inputIndex).SetTime(0);
            return;
        }

        if (inputIndex >= 0 && mixer.GetInputWeight(inputIndex) == 0)
        {
            mixer.GetInput(inputIndex).SetTime(0);
        }
        fadeIndex = inputIndex;
        fadeDuration = duration;
    }

    public void Switch(int index)
    {
        fadeIndex = -1;
        fadeDuration = 0;
        for (int i = 0; i < inputCount; i++)
        {
            mixer.SetInputWeight(i, i == index ? 1 : 0);
        }

        if (index >= 0)
            mixer.GetInput(index).SetTime(0);
    }

    public override void PrepareFrame(Playable playable, FrameData data)
    {
        if (fadeDuration > 0)
        {
            bool inProgress = false;
            float delta = data.deltaTime / fadeDuration;

            for (int i = 0; i < inputCount; i++) 
            {
                float weight = mixer.GetInputWeight(i);
                if (fadeIndex == i && weight < 1)
                {
                    // fade in
                    weight *= weight;
                    weight = Math.Min(weight + delta, 1);
                    weight = (float)Math.Sqrt(weight);
                    mixer.SetInputWeight(i, weight);

                    inProgress = true;
                }
                else if (weight > 0)
                {
                    // fade out
                    weight = Mathf.Sqrt(weight);
                    weight = Math.Max(weight - delta, 0);
                    weight = weight * weight;
                    mixer.SetInputWeight(i, weight);
                    inProgress = true;
                } 
            }

            if (!inProgress)
            {
                fadeDuration = 0;
            }
        }

        if (fadeDuration == 0 && fadeIndex == -1) 
        {
            playable.GetGraph().Stop();
        }
    }
}
