using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherCrystal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Crystal")
        {
            GetComponent<PlayerController>().Crystals += 1;
            Destroy(other.gameObject);
        }
    }
}
