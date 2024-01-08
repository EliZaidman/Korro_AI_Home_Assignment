using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapGameObject : MonoBehaviour
{
    [SerializeField] int trapDmg;

    private void OnCollisionEnter(Collision collision)
    {
        print("Found Collision");
        if (collision.gameObject.CompareTag("Player"))
        {
            print("Inside Player");
            PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.Hit(trapDmg);
                print("GET HITTTTTTTT");
            }
        }
    }
}
