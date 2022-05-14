using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleWaterCollision : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        ToggleWater(false);
    }
    public void ToggleWater(bool isOn)
    {
        foreach(Collider c in GetComponentsInChildren<MeshCollider>())
        {
            c.enabled = isOn;
        }
    }
}
