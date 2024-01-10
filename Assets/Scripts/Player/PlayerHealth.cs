using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{


    private int currentHealth;
    [SerializeField] private int startingHealth = 3;

    public static event Action<int> OnGettingHit; // Event for grounded state change
    public static event Action<int> SentHpOnGameStart; // Event for grounded state change

    // Define an event based on the delegate

    private void Start()
    {
        currentHealth = startingHealth;
        SentHpOnGameStart?.Invoke(startingHealth);
    }

    private void _OnGettingHit(int dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0)
        {
            string sceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(sceneName);
        }
    }

    public void Hit(int num)
    {
        OnGettingHit?.Invoke(num);
        _OnGettingHit(num);
    }
}
