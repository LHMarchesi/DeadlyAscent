using UnityEngine;

public class RightPunchCollision : MonoBehaviour
{
    Punches punches;

    private void Awake()
    {
        punches = GetComponentInParent<Punches>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!punches.IsPunching) return;

        IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();
        if (damagable != null)
        {
            damagable.GetDamage(punches.Damage);
            punches.IsPunching = false; // Prevents multiple hits during a single punch
        }
    }
}
