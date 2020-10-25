using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyDisplay : MonoBehaviour {

    private TextMeshProUGUI moneyText;

    // Start is called before the first frame update
    void Start() {
        moneyText = GetComponent<TextMeshProUGUI>();
        UpdateMoneyUI();
    }

    private void OnEnable() {
        moneyText = GetComponent<TextMeshProUGUI>();
        Inventory.MoneyChangedListener += UpdateMoneyUI;
    }

    private void OnDisable() {
        Inventory.MoneyChangedListener -= UpdateMoneyUI;
    }

    // Update is called once per frame

    private void UpdateMoneyUI() {
        moneyText.SetText(Inventory.money + "");
    }
}