using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    private TextMeshProUGUI crystalTxt;
    private TextMeshProUGUI switchesTxt;
    private TextMeshProUGUI exchangeWarningTxt;
    private TextMeshProUGUI portalNumberTxt;
    private TextMeshProUGUI crystalNumberTxt;

    void Awake()
    {
        crystalTxt = GameObject.FindGameObjectWithTag("CrystalUI").GetComponent<TextMeshProUGUI>();
        switchesTxt = GameObject.FindGameObjectWithTag("SwitchesUI").GetComponent<TextMeshProUGUI>();
        exchangeWarningTxt = GameObject.FindGameObjectWithTag("ExchangeWarning").GetComponent<TextMeshProUGUI>();
        portalNumberTxt = GameObject.FindGameObjectWithTag("PortalNumber").GetComponent<TextMeshProUGUI>();
        crystalNumberTxt = GameObject.FindGameObjectWithTag("CrystalNumber").GetComponent<TextMeshProUGUI>();
    }

    public void UpdateCrystalsCount(int count) => crystalTxt.SetText(count.ToString());
    public void UpdateDimensinSwitchesCount(int count) => crystalTxt.SetText(count.ToString());

    public void UpdateExchangeValues(int crystals, int portals)
    {
        crystalNumberTxt.SetText(crystals.ToString() + " X ");
        portalNumberTxt.SetText(portals.ToString() + " X ");
    }

    public void SetExchangeWarning(string message) 
    {
        exchangeWarningTxt.SetText(message);
        StartCoroutine(DeactivateWarning(5f));
    }

    IEnumerator DeactivateWarning(float waitTime) 
    {
        yield return new WaitForSeconds(waitTime);
        exchangeWarningTxt.SetText(" ");
    }
   
}
