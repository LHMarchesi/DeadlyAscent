using UnityEngine;
using UnityEngine.Pool;

public class BaseProjectil : MonoBehaviour
{
    [SerializeField] ProjectilScriptable projectilData;
    public ObjectPool<GameObject> pool;
    private float timeOutDealay = 5f;

    public virtual void OnEnable()
    {
        Invoke("Deactivate", timeOutDealay);
    }

    protected void Deactivate()
    {
        pool?.Release(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();
        if (damagable != null)
        {
            damagable.GetDamage(projectilData.BaseDamage);
        }
        Deactivate();
    }
}
