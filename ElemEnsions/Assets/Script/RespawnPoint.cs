using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;
        if (go.CompareTag("Player"))
        {
            go.GetComponent<PlayerRespawn>().SetRespawnPoint(transform.position);
        }
    }
}
