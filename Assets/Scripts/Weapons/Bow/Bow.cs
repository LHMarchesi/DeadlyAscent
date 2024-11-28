using UnityEngine;
using UnityEngine.Pool;

public class Bow : MonoBehaviour
{
    [Tooltip("Prefab to shoot")]
    [SerializeField] private Arrow arrowPrefab;
    [Tooltip("Projectile force")]
    [SerializeField] private float shootingForce;
    [Tooltip("End point of gun where shots appear")]
    [SerializeField] private Transform shootPosition;
    [Tooltip("Time between shots / smaller = higher rate of fire")]
    [SerializeField] private float cooldownWindow;

    private float nextTimeToShoot;

    ObjectPool<Arrow> arrowPool;

    private void Awake()
    {
        arrowPool = new ObjectPool<Arrow>(CreatePoolItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, false, 10, 100);
    }

    private void OnDestroyPoolObject(Arrow arrow)
    {
        Destroy(arrow.gameObject);
    }

    private void OnReturnedToPool(Arrow arrow)
    {
        arrow.gameObject.SetActive(false);
    }

    private void OnTakeFromPool(Arrow arrow)
    {
        arrow.gameObject.SetActive(true);
    }

    private Arrow CreatePoolItem()
    {
        Arrow arrow = Instantiate(arrowPrefab);
        arrow.gameObject.SetActive(false);
        arrow.pool = arrowPool;
        return arrow;
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextTimeToShoot)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Arrow arrowInstance = arrowPool.Get();

        arrowInstance.transform.SetPositionAndRotation(shootPosition.position, Quaternion.identity);

        // Apply force to the projectile
        arrowInstance.GetComponent<Rigidbody>().AddForce(shootPosition.forward.normalized * shootingForce, ForceMode.Impulse);

        // Set the cooldown for the next shot
        nextTimeToShoot = Time.time + cooldownWindow;
    }
}
