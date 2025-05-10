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

    //private Coroutine spawnCoroutine;

    //private void Start()
    //{
    //    StartCoroutine(SpawnLoop());
    //}

    //public void StartSpawning()
    //{
    //    if (spawnCoroutine == null)
    //        spawnCoroutine = StartCoroutine(SpawnLoop());
    //}

    //public void StopSpawning()
    //{
    //    if (spawnCoroutine != null)
    //    {
    //        StopCoroutine(spawnCoroutine);
    //        spawnCoroutine = null;
    //    }
    //}


    IEnumerator SpawnLoop()
    private bool isActive = false;
    IEnumerator SpawnLoop(int Stage)
    {
        while (isActive)
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
                
            }
            yield return new WaitForSeconds(spawnTime);
        }
    }

    //스테이지 변경 연결
    private void OnEnable()
    {
        EventManager.AddListener<StageChangeEvent>(OnSpawnEnemy);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<StageChangeEvent>(OnSpawnEnemy);
    }

    private void OnSpawnEnemy(StageChangeEvent evt)
    {
        ActiveSwitch(evt);
    }

    public void ActiveSwitch(StageChangeEvent evt)
    {
        switch (evt.EventType)
        {
            case StageChangeEventType.Start:
                isActive = true;
                StartCoroutine(SpawnLoop(evt.Stage));
                spawnCount = evt.Stage+5;
                break;
            case StageChangeEventType.SpawnEnd:
                isActive = false;
                StopCoroutine(SpawnLoop(evt.Stage));
                break;
        }
    }
}
