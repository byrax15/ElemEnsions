using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{
    private const int PORTALS_PACK = 3;
    private int crystals = 0;
    private int dimensionSwitches = 0;
    private int exchangePortal = 0;
    private int exchangeCrystal = 0;

    private DimensionChangeMediator _dimensionMediator;

    private void Start()
    {
        _dimensionMediator = GameObject.FindGameObjectWithTag("DimensionMediator")
            .GetComponent<DimensionChangeMediator>();
    }

    public int Crystals
    {
        get => crystals;

        set
        {
            crystals = value;
            GetComponent<PlayerUI>().UpdateCrystalsCount(crystals);
        }
    }

    public int DimensionSwitches
    {
        get => dimensionSwitches;

        set
        {
            dimensionSwitches = value;
            GetComponent<PlayerUI>().UpdateDimensinSwitchesCount(dimensionSwitches);
        }
    }


    public bool PrepareExchange()
    {
        exchangePortal = 0;
        exchangeCrystal = 0;

        if (crystals < PORTALS_PACK)
        {
            GetComponent<PlayerUI>().SetExchangeWarning("Nombre de cristaux insuffisant, achat impossible.");
            GetComponent<PlayerUI>().UpdateExchangeValues(exchangeCrystal, exchangePortal);

            return false;
        }

        exchangePortal = Crystals / PORTALS_PACK;
        exchangeCrystal = exchangePortal * PORTALS_PACK;

        GetComponent<PlayerUI>().UpdateExchangeValues(exchangeCrystal, exchangePortal);
        return true;
    }

    public void ConfirmExchange()
    {
        DimensionSwitches += exchangePortal;
        Crystals -= exchangeCrystal;
    }

    public void UseDimensionSwitch(InputAction.CallbackContext callback)
    {
        if (callback.started)
        {
            
            if (DimensionSwitches <= 0)
                return;
            
            if (!callback.action.name.TryGetContainedDimension(out var dimension))
                return;

            if (_dimensionMediator.TryChangeDimension(dimension))
                DimensionSwitches--;
        }
    } 
}