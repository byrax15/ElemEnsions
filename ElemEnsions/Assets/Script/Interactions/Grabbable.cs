using UnityEngine;

public class Grabbable : Interactable
{
    [SerializeField] private Transform _playerHand;
    [SerializeField] private PlayerController _playerController;
    
    public override bool Interact()
    {
        Grab();
        return true;
    }

    private void Grab()
    {
        if (_playerController.HeldItem != null)
        {
            _playerController.HeldItem.transform.parent = null;
        }
        
        _playerController.HeldItem = gameObject;
        _playerController.HeldItem.transform.parent = _playerHand;
        _playerController.HeldItem.transform.position = _playerHand.position;
    }
}
