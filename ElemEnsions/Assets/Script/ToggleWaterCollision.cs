using UnityEngine;

public class ToggleWaterCollision : MonoBehaviour
{
    private void Start()
    {
        ToggleWater(false);
    }

    private void ToggleWater(bool isOn)
    {
        foreach(MeshCollider meshCollider in GetComponentsInChildren<MeshCollider>())
        {
            meshCollider.enabled = isOn;
        }
    }
}
