using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    public GameObject bulletPrefab; // bullet prefab
    public Transform firePoint; // firepoint for bullet
    public float bulletSpeed = 15f; // bullet speed
    public float fireRate = 0.5f; // the time between shots
    public float bulletLifetime = 2f; // how long bullets live before despawning

    void Start()
    {
        if (bulletPrefab == null || firePoint == null)
        {
            Debug.LogWarning(name + ": ShootingEnemy is missing a bullet prefab or fire point.", this);
            return;
        }

        InvokeRepeating(nameof(Shoot), 2f, fireRate); // Shoots every few seconds
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        if (bullet.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.velocity = firePoint.forward * bulletSpeed;
        }

        Destroy(bullet, bulletLifetime); // Clean up the bullet after its lifetime
    }
}
