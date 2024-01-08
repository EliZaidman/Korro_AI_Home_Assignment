using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public static event Action<int> OnCollectingKey;

     void KeyCollected()
    {
        OnCollectingKey?.Invoke(0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            KeyCollected();
            Destroy(gameObject);
        }
    }
}
