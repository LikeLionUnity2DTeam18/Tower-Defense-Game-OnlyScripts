using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public EnemyData testEnemy;
    public Transform[] spawnPoints;
    [SerializeField] private float spawnTime = 2f;
    [SerializeField] private int spawnCount = 10; // �ѹ��� ������ ���� ��

    public EnemyFactory factory;

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

                // �ణ ��ġ ���� �ָ� ��ġ�� �ʰ� ��
                Vector3 offset = new Vector3(Random.Range(-0.4f, 0.4f), Random.Range(-0.4f, 0.4f), 0);

                if (testEnemy != null)
                {
                    factory.createEenemy(testEnemy, spawnPos + offset);
                }
                else
                {
                    Debug.LogWarning("testEnemy ��� ����! EnemyData �־���.");
                }
            }
            yield return new WaitForSeconds(spawnTime);
        }
    }

}
