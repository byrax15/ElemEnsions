using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
         GameObject go = other.gameObject;
        if (go.CompareTag("Player"))
        {
            go.GetComponent<PlayerRespawn>().Respawn();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {

    }
}
