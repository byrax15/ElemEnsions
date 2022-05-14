using System.Collections;
using UnityEngine;

public class Flammable : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        StartCoroutine(Burn());
    }
    
    private IEnumerator Burn()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
