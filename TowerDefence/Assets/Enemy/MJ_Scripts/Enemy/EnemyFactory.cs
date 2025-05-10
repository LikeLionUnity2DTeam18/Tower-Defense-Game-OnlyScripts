using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public EnemyController CreateEnemy(EnemyData data, Vector3 spawnPosition)
    {
        GameObject obj = Instantiate(data.enemyPrefab, spawnPosition, Quaternion.identity);
        EnemyController enemy = obj.GetComponent<EnemyController>();

        if (enemy != null)
        {
            EnemyData clonedData = data.Clone();  // ✅ 복제본 사용
            enemy.Initialize(clonedData);
        }
        else
        {
            Debug.LogError("[EnemyFactory] EnemyController가 프리팹에 없습니다!");
        }

        // 이벤트 발사
        EventManager.Trigger(new EnemySpawned(enemy.transform));
        return enemy;
    }
}