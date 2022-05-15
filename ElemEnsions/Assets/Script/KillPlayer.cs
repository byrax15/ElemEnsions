using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip[] deathSound;
    
    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;
        if (go.CompareTag("Player"))
        {
            source.PlayOneShot(deathSound[(int)Random.Range(0, (float)deathSound.Length)], 0.6f);
            
            go.GetComponent<PlayerRespawn>().Respawn();
        }
    }
}
