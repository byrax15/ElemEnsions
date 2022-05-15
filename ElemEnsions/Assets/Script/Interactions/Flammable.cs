using System.Collections;
using UnityEngine;

public class Flammable : Interactable
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private float _burningDuration = 3.0f;

    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip burnAudio;
    
    private static readonly int progress = Shader.PropertyToID("_Progress");

    private const string TORCH_TAG = "Torch";

    public override bool Interact()
    {
        return Burn();
    }

    private bool Burn()
    {
        if (_playerController.HeldItem != null && _playerController.HeldItem.CompareTag(TORCH_TAG))
        {
            Destroy(_playerController.HeldItem);
            _playerController.HeldItem = null;
            StartCoroutine(PlayBurnEffect());
            return true;
        }
        
        return false;
    }
    
    private IEnumerator PlayBurnEffect()
    {
        StartCoroutine(BurnSound());
        for (float time = 0f; time < _burningDuration; time += Time.deltaTime)
        {
            GetComponent<Renderer>().material.SetFloat(progress, Mathf.Lerp(1, 0, time / _burningDuration));
            yield return null;
        }
        
        GetComponent<Renderer>().material.SetFloat(progress, 0);
        Destroy(gameObject);
    }

    private IEnumerator BurnSound()
    {
        audioSource.PlayOneShot(burnAudio, 0.5f);
        yield return null;
    }
}
