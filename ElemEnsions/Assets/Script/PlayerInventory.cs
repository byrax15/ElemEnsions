using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    private const int PORTALS_PACK = 3;
    private int crystals = 0;
    private int dimensionSwitches = 0;
    private int exchangePortal = 0;
    private int exchangeCrystal = 0;

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

        if(crystals < PORTALS_PACK)
        {
            GetComponent<PlayerUI>().SetExchangeWarning("Vous n'avez pas assez de cristaux pour faire un échange.");
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

    public void UseDimensionSwitch()
    {
        if(DimensionSwitches > 0)
            DimensionSwitches--;
    }
}
