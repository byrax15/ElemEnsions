using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public GameObject InteractionIndicatorPrefab;
    public bool CanInteractMultipleTime;
    
    // Return true if interaction is done successfully, else false
    public abstract bool Interact();
}