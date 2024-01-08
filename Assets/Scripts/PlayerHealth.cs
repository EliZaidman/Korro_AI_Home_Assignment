using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{


   [SerializeField] private int currentHealth;
   [SerializeField] private int maxHealth;

    public static event Action<int> OnGettingHit; // Event for grounded state change

    // Define an event based on the delegate

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void _OnGettingHit(int dmg)
    {
        currentHealth -= dmg;
    }

    public void Hit(int num)
    {
        OnGettingHit?.Invoke(num);
        _OnGettingHit(num);
    }
}
