using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class TestAudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource m_source = null;
 
    [SerializeField]
    private AudioClip[] m_clips = null;

    [SerializeField] private float duration = 0.0f;

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

    public void toto(InputAction.CallbackContext ctx) 
    {
        if (ctx.performed) {
            // graph.Crossfade(1, duration);
            graph.DecreaseVolume(0.85f);
        }
            
            // graph.Switch(1);
    }
    
    private void Update()
    {
        // if (Input.GetKeyDown("space"))
        // {
            
        // }
        // int clipIndex = 1;
        // float fadeTime = 80.0f;
        // graph.Crossfade(clipIndex, fadeTime);
    }
}
