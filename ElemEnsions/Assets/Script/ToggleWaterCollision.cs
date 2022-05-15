using UnityEngine;
using Script;
using System;

public class ToggleWaterCollision : MonoBehaviour
{
    [SerializeField]private DimensionChangeMediator mediator;
    private void Start()
    {
        mediator.AddListener(DimensionChange);
        ToggleWater(false);
    }

    private void DimensionChange(Dimension oldDim, Dimension newDim)
    {
        ToggleWater(newDim == Dimension.Water);
    }

    private void ToggleWater(bool isOn)
    {
        foreach(MeshCollider meshCollider in GetComponentsInChildren<MeshCollider>())
        {
            meshCollider.isTrigger = !isOn;
        }
    }
}
