using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCanon : MonoBehaviour
{
    public GameObject bulletPrefab; // Manualy need to choose bullet(trap)
    public float shootForce = 1000f;
    public Transform shootPoint; // The point from which the bullet is shot
    public float shootDelay = 2f; // Time delay between shots

    private float shootTimer; // Timer to track shooting delay
    public int trapDmg = 1;
    void Update()
    {
        // Update the timer
        shootTimer -= Time.deltaTime;

        // Check if it's time to shoot
        if (shootTimer <= 0)
        {
            ShootBullet();
            shootTimer = shootDelay; // Reset the timer
        }
    }

    void ShootBullet()
    {
        if (bulletPrefab != null && shootPoint != null)
        {
            // Create a new bullet instance at the shootPoint position and rotation
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            //Override bullet damage
            bullet.GetComponent<TrapGameObject>().ChangeTrapDamage(trapDmg);
            // Add force to the bullet's Rigidbody to propel it forward
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(shootPoint.up * shootForce);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
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
}
