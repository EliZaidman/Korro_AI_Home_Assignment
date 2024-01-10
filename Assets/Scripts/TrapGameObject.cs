using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrapGameObject : MonoBehaviour
{
    int trapDmg;
    public float upTime;
    private void Start()
    {
        Destroy(gameObject, upTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        print("Found Collision");
        if (collision.gameObject.CompareTag("Player"))
        {
            print("Inside Player");
            PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.Hit(trapDmg);
            }
        }
    }
    public void ChangeTrapDamage(int damage)
    {
        trapDmg = damage;
    }
}
