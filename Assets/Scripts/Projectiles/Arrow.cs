using UnityEngine;
using UnityEngine.Pool;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float timeoutDelay = 3f;
    [SerializeField] private float damage;

    public ObjectPool<Arrow> pool;

    void OnEnable()
    {
        Invoke("Deactivate",timeoutDelay);
    }
    
    private void Deactivate()
    {
        pool.Release(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();
        if (damagable != null)
        {
            damagable.GetDamage(damage);
            Debug.Log($"Dealt {damage} damage to {collision.gameObject.name}");
            Deactivate(); // Desactiva el proyectil después de hacer daño
        }
    }
}
