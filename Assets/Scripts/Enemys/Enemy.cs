using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy Data/Enemy")]
public class Enemy : ScriptableObject
{
    public string Name;
    public float Health;
    public float Armor;
    public float Speed;
    public float BaseDamage;
    public float AttackSpeed;
}
