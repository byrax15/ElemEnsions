using System.Collections;
using UnityEngine;

public class Flammable : Interactable
{
    [SerializeField] private PlayerController _playerController;
    
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
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
