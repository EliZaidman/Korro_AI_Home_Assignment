using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static event Action<int> OnCoinCollected; // Event for grounded state change

    int trapDmg;
    public int coinValue;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
            if (player != null)
            {
                OnCoinCollected?.Invoke(coinValue);
                Destroy(gameObject);
            }
        }
    }
}
