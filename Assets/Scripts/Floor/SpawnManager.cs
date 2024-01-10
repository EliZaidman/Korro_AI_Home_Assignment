using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    #region Singelton
    public static SpawnManager Instance { get; private set; }
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

    public static event Action<GameObject> OnTouchingFloor; // Event for grounded state change

    public List<Transform> spawnPoint;

    public GameObject player;

    int currentSpawn = 0;
    private void Start()
    {
        player.transform.position = spawnPoint[currentSpawn].position;
    }

    public void Respawn()
    {
        player.transform.position = spawnPoint[currentSpawn].position;
    }
}
