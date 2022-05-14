using UnityEngine;
using System.Collections;

public class TestAudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource m_source = null;
 
    [SerializeField]
    private AudioClip[] m_clips = null;

    private AudioManagerGraph graph;

    private void Awake()
    {
        graph = new AudioManagerGraph();
        graph.Create(m_source, m_clips);
        graph.Play();
    }
    
    private void OnDestroy()
    {
        graph.Destroy();
        graph = null;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            graph.Crossfade(1, 10.0f);
        }
        // int clipIndex = 1;
        // float fadeTime = 80.0f;
        // graph.Crossfade(clipIndex, fadeTime);
    }
}
