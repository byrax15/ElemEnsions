using UnityEngine;

public class GatherCrystal : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip gatherSound;
    
    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player")
        {
            source.PlayOneShot(gatherSound, 0.4f);
            other.gameObject.GetComponent<PlayerInventory>().Crystals += 1;
            Destroy(gameObject);
        }
    }
}
