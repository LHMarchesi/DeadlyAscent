using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Gun : MonoBehaviour
{
    [Tooltip("Prefab to shoot")]
    [SerializeField] private Projectile projectilePrefab;
    [Tooltip("Projectile force")]
    [SerializeField] private float muzzleVelocity = 700f;
    [Tooltip("End point of gun where shots appear")]
    [SerializeField] private Transform muzzlePosition;
    [Tooltip("Time between shots / smaller = higher rate of fire")]
    [SerializeField] private float cooldownWindow = 0.1f;

    private float nextTimeToShoot;

    ObjectPool<Projectile> projectilePool;

    private void Awake()
    {
        projectilePool = new ObjectPool<Projectile>(CreatePoolItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, false, 10, 100);
    }

    private void OnDestroyPoolObject(Projectile projectile)
    {
        Destroy(projectile.gameObject);
    }

    private void OnReturnedToPool(Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
    }

    private void OnTakeFromPool(Projectile projectile)
    {
        projectile.gameObject.SetActive(true);
    }

    private Projectile CreatePoolItem()
    {
        Projectile projectile = Instantiate(projectilePrefab);
        projectile.gameObject.SetActive(false);
        projectile.pool = projectilePool;
        return projectile;
    }

    void FixedUpdate()
    {
        if (Input.GetButton("Fire1") && Time.time > nextTimeToShoot)
        {

            Projectile projectileInstance = projectilePool.Get();

            projectileInstance.transform.SetPositionAndRotation(muzzlePosition.position, muzzlePosition.rotation);

            projectileInstance.GetComponent<Rigidbody>().AddForce(projectileInstance.transform.forward * muzzleVelocity, ForceMode.Acceleration);

            nextTimeToShoot = Time.time + cooldownWindow;
        }
    }

}
