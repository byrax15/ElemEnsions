using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 respawnPoint;

    public void Respawn()
    {
        StartCoroutine(nameof(respawnCoroutine));
        transform.position = respawnPoint;
    }

    public void SetRespawnPoint(Vector3 point)
    {
        respawnPoint = point;
    }

    IEnumerator respawnCoroutine()
    {
        GetComponent<PlayerController>().enabled = false;
        yield return new WaitForSeconds(1);
        GetComponent<PlayerController>().enabled = true;
    }
}
