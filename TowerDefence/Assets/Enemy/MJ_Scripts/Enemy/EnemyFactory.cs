using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public EnemyController CreateEnemy(EnemyData data, Vector3 spawnPosition)
    {
        GameObject obj = Instantiate(data.enemyPrefab, spawnPosition, Quaternion.identity);
        EnemyController enemy = obj.GetComponent<EnemyController>();

        if (enemy != null)
        {
            enemy.Initialize(data);
        }
        else
        {
            Debug.LogError("[EnemyFactory] EnemyController가 프리팹에 없습니다!");
        }

        return enemy;
    }
}