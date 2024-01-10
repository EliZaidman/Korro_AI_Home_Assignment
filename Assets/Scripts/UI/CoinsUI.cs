using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsUI : MonoBehaviour
{
    #region Singelton
    public static CoinsUI Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion
    private TextMeshProUGUI coinCounter;
    public static event Action<Vector3> OnCoinCollected; // Event for grounded state change

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
        coinCounter.text = $"Coins: {counter}";
    }

    public TextMeshProUGUI getCoinCounter() { return coinCounter; }
}
