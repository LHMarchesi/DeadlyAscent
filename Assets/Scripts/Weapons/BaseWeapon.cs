using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    [SerializeField] protected WeaponScriptable weaponData;

    protected float nextTimeToShoot;
    public abstract void Shoot();

    public void Drop()
    {
    }

    public void PickUp()
    {
    }

    protected bool CanShoot()
    {
        return Time.time >= nextTimeToShoot;
    }

}
