using UnityEngine;

public abstract class EnemyAbstractFactory : MonoBehaviour
{
    public abstract GameObject CreateEnemy(Vector3 position);
}
