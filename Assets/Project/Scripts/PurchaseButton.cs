using System;
using System.Collections;
using System.Collections.Generic;
using UI.View;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PurchaseButton : MonoBehaviour
{
    [SerializeField] private InventoryView inventoryView;
    [SerializeField] private int coins;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => inventoryView.Purchase(coins));
    }
}
