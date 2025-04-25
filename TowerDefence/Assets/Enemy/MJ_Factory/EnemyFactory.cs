using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public GameObject CreateEnemy(EnemyData data, Vector2 spawnPosition)
    {
        GameObject enemy = Instantiate(data.enemyPrefab, spawnPosition, Quaternion.identity);

        EnemyController controller = enemy.GetComponent<EnemyController>();
        if(controller != null)
        {
            controller.Initialize(data);
        }

        return enemy;
    }
}
