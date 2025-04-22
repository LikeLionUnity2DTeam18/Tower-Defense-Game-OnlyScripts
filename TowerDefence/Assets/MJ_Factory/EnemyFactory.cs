using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public GameObject createEenemy(EnemyData data, Vector2 spawnPosition)
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
