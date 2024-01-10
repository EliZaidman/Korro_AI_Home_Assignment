using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{


    [SerializeField] private int startingHealth = 3;
    private int currentHealth;

    public static event Action<int> OnGettingHit;
    public static event Action<int> SentHpOnGameStart;

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
            //Reload Scene
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
