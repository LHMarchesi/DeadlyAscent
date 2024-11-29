using UnityEngine;

[CreateAssetMenu(fileName = "New Projectil", menuName = "Projectil Data/Projectil")]

public class ProjectilScriptable : ScriptableObject
{
    public string Name;
    public float BaseDamage;
}
