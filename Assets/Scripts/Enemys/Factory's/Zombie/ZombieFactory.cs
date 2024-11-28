using UnityEngine;

public class ZombieFactory : EnemyAbstractFactory
{
    [SerializeField] GameObject ZombiePrefab;

    public ZombieFactory(GameObject zombiePrefab)
    {
        ZombiePrefab = zombiePrefab;
    }

    public override GameObject CreateEnemy(Vector3 position)
    {
        return Instantiate(ZombiePrefab, position, Quaternion.identity, this.transform);
    }
}
