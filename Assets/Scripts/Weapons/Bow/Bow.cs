using UnityEngine;
using UnityEngine.Pool;

public class Bow : BaseWeapon
{
    ObjectPool<GameObject> arrowPool;

    private void Awake()
    {
      //  arrowPool = new ObjectPool<GameObject>(CreatePoolItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, false, 10, 100);
    }

    private void OnDestroyPoolObject(GameObject arrow)
    {
        Destroy(arrow);
    }

    private void OnReturnedToPool(GameObject arrow)
    {
        arrow.SetActive(false);
    }

    private void OnTakeFromPool(GameObject arrow)
    {
        arrow.SetActive(true);
        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

   /* private GameObject CreatePoolItem()
    {
       // GameObject arrow = Instantiate(weaponData.projectil);
        BaseProjectil baseProjectil = arrow.GetComponent<BaseProjectil>();
        baseProjectil.pool = arrowPool;
        arrow.SetActive(false);
        return arrow;
    }*/

    private void Update()
    {
        if (Input.GetButton("Fire1") && CanShoot())
        {
            Shoot();
        }
    }

    public override void Shoot()
    {
        GameObject arrowInstance = arrowPool.Get();
     //   arrowInstance.transform.SetPositionAndRotation(shootPosition.position, Quaternion.identity);

        Rigidbody rb = arrowInstance.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
           // rb.AddForce(shootPosition.forward * weaponData.shootingForce, ForceMode.Impulse);
        }

        nextTimeToShoot = Time.time + weaponData.cooldownWindow;
        // OnShoot?.Invoke(); // Notifica a otros sistemas
    }
}
