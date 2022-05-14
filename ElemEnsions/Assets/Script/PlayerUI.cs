using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    private TextMeshProUGUI crystalTxt;

    void Awake()
    {
        crystalTxt = GameObject.FindGameObjectWithTag("CrystalUI").GetComponent<TextMeshProUGUI>();
    }

    public void UpdateCrystalsCount(int count) => crystalTxt.SetText(count.ToString());
   
}
