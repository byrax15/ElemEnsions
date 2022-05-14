using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    private int crystals = 0;
    private int dimensionSwitches = 0;

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


    public void ExchangeCrystal(int qnt)
    {
        if(qnt > crystals)
        {
            GetComponent<PlayerUI>().SetExchangeWarning("Vous n'avez pas assez de cristaux pour faire cet échange.");
        }
        else
        {
            DimensionSwitches += qnt;
            Crystals -= qnt;
        }
    }
}
