using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class CoinsUI : MonoBehaviour
{
    TextMeshProUGUI coinCounter;
    int counter = 0;
    private void Start()
    {
        coinCounter = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        Coin.OnCoinCollected += Coin_OnCoinCollected;
    }

    private void OnDisable()
    {
        Coin.OnCoinCollected -= Coin_OnCoinCollected;

    }
    private void Coin_OnCoinCollected(int obj)
    {
        counter += obj;
        coinCounter.text = $"Coins - {counter}";
    }
}
