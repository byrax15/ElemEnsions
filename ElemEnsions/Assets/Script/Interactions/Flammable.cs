using System.Collections;
using UnityEngine;

public class Flammable : Interactable
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private float _burningDuration = 3.0f;
    
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
            StartCoroutine(PlayBurnEffect());
            return true;
        }
        
        return false;
    }
    
    private IEnumerator PlayBurnEffect()
    {
        for (float time = 0f; time < _burningDuration; time += Time.deltaTime)
        {
            GetComponent<Renderer>().material.SetFloat(progress, Mathf.Lerp(1, 0, time / _burningDuration));
            yield return null;
        }
        
        GetComponent<Renderer>().material.SetFloat(progress, 0);
        Destroy(gameObject);
    }
}
