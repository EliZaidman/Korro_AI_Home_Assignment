using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorPlatform : MonoBehaviour
{
    private void OnEnable()
    {
        SpawnManager.OnTouchingFloor += RespawnPlayer;
    }

    private void OnDisable()
    {
        SpawnManager.OnTouchingFloor -= RespawnPlayer;
    }

    private void RespawnPlayer(GameObject obj)
    {
        throw new NotImplementedException();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("SPAWN PLAYER");
            SpawnManager.Instance.Respawn();
        }
    }
}
