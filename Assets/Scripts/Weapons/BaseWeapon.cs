using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour, IWeapon
{
    [SerializeField] protected WeaponScriptable weaponData;
    [SerializeField] protected Transform shootPosition;

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
