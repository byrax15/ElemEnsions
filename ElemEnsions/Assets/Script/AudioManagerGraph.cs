using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Playables;
 

public class AudioManagerGraph : MonoBehaviour
{
    private PlayableGraph graph;
    private AudioClipPlayable[] clipPlayables;
    private AudioMixerPlayable mixerPlayable;
    private AudioPlayableOutput output;
    private AudioManager audioManager;
    private ScriptPlayable<AudioManager> audioManagerPlayable;


    public bool IsPlaying { get => graph.IsValid() && graph.IsPlaying(); }

    public double Time { get => audioManagerPlayable.GetTime(); }


    public void Create(AudioSource audioSource, AudioClip[] clips)
    {
        Destroy();

        graph = PlayableGraph.Create("AudioManagerGraph");
        graph.SetTimeUpdateMode(DirectorUpdateMode.DSPClock);
        graph.Stop();

        mixerPlayable = AudioMixerPlayable.Create(graph, clips.Length);
        
        clipPlayables = new AudioClipPlayable[clips.Length];
        for (int i = 0; i < clips.Length; i++)
        {
            clipPlayables[i] = AudioClipPlayable.Create(graph, clips[i], true);
            mixerPlayable.ConnectInput(i, clipPlayables[i], 0, i == 0 ? 1 : 0);
        }

        audioManagerPlayable = AudioManager.Create(graph, mixerPlayable);
        audioManager = audioManagerPlayable.GetBehaviour();

        output = AudioPlayableOutput.Create(graph, "AudioManager", audioSource);
        output.SetSourcePlayable(audioManagerPlayable);
    }

    public void Destroy()
    {
        if (graph.IsValid())
            graph.Destroy();
    }

    public void Crossfade(int index, float duration)
    {
        if (index >= 0 && !graph.IsPlaying())
            graph.Play();
        audioManager.Crossfade(index, duration);
    }

    public void Switch(int index) 
    {
        Crossfade(index, 0.5f);
    }

    public void DecreaseVolume(float percent)
    {
        if (!graph.IsPlaying())
            graph.Play();
        audioManager.DecreaseVolume(percent);
    }

    public void Play()
    {
        if (!graph.IsPlaying())
            graph.Play();
    }


    public void Pause()
    {
        if (graph.IsPlaying())
            graph.Stop();
    }
}
