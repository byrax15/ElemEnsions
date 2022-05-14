using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherCrystal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerInventory>().Crystals += 1;
            Destroy(gameObject);
        }
    }
}
