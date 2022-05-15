using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExchangePortal : Interactable
{
    private MenuManager menuManager;

    void Start() 
    {
        menuManager = GameObject.FindGameObjectWithTag("MenuManager").GetComponent<MenuManager>();    
    }

    public override bool Interact()
    {
        menuManager.ExchangeCrystals();

        return true;
    }


}
