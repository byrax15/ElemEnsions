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


    public void PrepareExchange()
    {
        int portalsValue = 0;
        exchangeCrystal = 0;

        if(crystals < PORTALS_PACK)
            GetComponent<PlayerUI>().SetExchangeWarning("Vous n'avez pas assez de cristaux pour faire un échange.");
        else
        {
            exchangePortal = Crystals / PORTALS_PACK;
            exchangeCrystal = portalsValue / PORTALS_PACK;

            GetComponent<PlayerUI>().UpdateExchangeValues(exchangeCrystal, exchangePortal);
            
        }
        GetComponent<PlayerUI>().UpdateExchangeValues(exchangeCrystal, exchangePortal);
    }

    public void ConfirmExchange()
    {
        DimensionSwitches += exchangePortal;
        Crystals -= exchangeCrystal;
    }
}
