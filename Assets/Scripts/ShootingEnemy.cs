using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    public GameObject bulletPrefab; // bullet prefeb
    public Transform firePoint; // firepoint for bullet
    public float bulletSpeed = 15f; // bullet speed
    public float fireRate = 0.5f; // the time between shots

    void Start()
    {
        InvokeRepeating("Shoot", 2f, fireRate); // Shoots every few seconds
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = firePoint.forward * bulletSpeed;
        Destroy(bullet, 2f); // Destroy bullet after 5 seconds
    }
}
