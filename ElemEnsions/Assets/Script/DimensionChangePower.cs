using Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionChangePower : MonoBehaviour
{
    public DimensionChangeMediator mediator;
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        mediator.AddListener(ChangePowers);
        playerController = transform.parent.GetComponent<PlayerController>();
    }

    private void ChangePowers(Dimension oldDim, Dimension newDim)
    {
        if(oldDim != newDim)
            playerController.UpdatePowers(newDim);
    }
}
