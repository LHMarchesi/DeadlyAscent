using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon Data/Weapon")]

public class WeaponScriptable : ScriptableObject
{
    public string Name;
    public float BaseDamage;
    public float cooldownWindow;
    public GameObject projectil;
    public float shootingForce;
}
