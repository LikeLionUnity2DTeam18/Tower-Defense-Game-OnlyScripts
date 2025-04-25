using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public List<EnemyData> enemyTypes;
    public Transform[] spawnPoints;
    public EnemyFactory factory;

    [SerializeField] private float spawnTime = 2f;
    [SerializeField] private int spawnCount = 5; // 한번에 생성할 적의 수


    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        yield return new WaitForSeconds(1f);

        while (true)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                int index = Random.Range(0, spawnPoints.Length);
                Vector3 spawnPos = spawnPoints[index].position;

                // 약간 위치 차이 주면 겹치지 않게 됨
                Vector3 offset = new Vector3(Random.Range(-0.4f, 0.4f), Random.Range(-0.4f, 0.4f), 0);

                if (enemyTypes != null && enemyTypes.Count > 0)
                {
                    // 랜덤하게 적군 선택
                    EnemyData selectedEnemy = enemyTypes[Random.Range(0, enemyTypes.Count)];

                    // 적 생성
                    factory.CreateEnemy(selectedEnemy, spawnPos + offset);
                }
                else
                {
                    Debug.LogWarning("enemyTypes 리스트가 비어있어! 적 데이터 넣어줘.");
                }
            }
            yield return new WaitForSeconds(spawnTime);
        }
    }

}
