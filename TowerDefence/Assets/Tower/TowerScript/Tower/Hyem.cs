using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Hyem : Tower
{
    [Header("투사체")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform[] firePoints;
    [SerializeField] private float delayBetweenShots = 0.2f;

    [Header("특수스킬")]
    [SerializeField] private GameObject icePillarPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private int waveCount = 5;
    [SerializeField] private int pillarsPerLine = 5;        // 각 줄당 기둥 개수
    [SerializeField] private float maxDistance = 5f;
    [SerializeField] private float spreadAngle = 10f;       // 파동 퍼짐 각도
    [SerializeField] private float minScale = 0.5f;         // 가장 가까운 기둥 크기
    [SerializeField] private float maxScale = 1.5f;         // 가장 먼 기둥 크기
    public override void Awake()
    {
        base.Awake();

        fsmLibrary = new FSMLibrary(this, towerFSM);
        idleState = fsmLibrary.hIdleState;
        moveState = fsmLibrary.hMoveState;
        meleeState = fsmLibrary.hMeleeState;
        rangeState = fsmLibrary.hRangeState;
        specialState = fsmLibrary.hSpecialState;
    }
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    public void StartShooting(Vector2 targetPos)
    {
        StartCoroutine(Shoot(targetPos));
    }

    private IEnumerator Shoot(Vector2 targetPos)
    {
        foreach (var point in firePoints)
        {
            Vector2 dir = (targetPos - (Vector2)point.position).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            GameObject spear = PoolManager.Instance.Get(projectile);
            spear.transform.position = point.position;
            spear.GetComponent<TowerProjectile>().Init(dir);

            yield return new WaitForSeconds(delayBetweenShots);
        }
    }


    public void CastIceCone()
    {
        StartCoroutine(IceConeCoroutine());
    }

    private IEnumerator IceConeCoroutine()
    {
        // N갈래 방향 계산
        Vector2[] waveDirs = new Vector2[waveCount];
        float angleStep = spreadAngle / (waveCount - 1);
        float startAngle = -spreadAngle / 2f;

        for (int i = 0; i < waveCount; i++)
        {
            float angleOffset = startAngle + (angleStep * i);
            waveDirs[i] = Quaternion.Euler(0, 0, angleOffset) * dir;
        }

        // 거리별 생성
        for (int j = 0; j < pillarsPerLine; j++)
        {
            float distance = Mathf.Lerp(1f, maxDistance, (float)j / (pillarsPerLine - 1));

            // 거리 비례 크기 계산
            float scale = Mathf.Lerp(minScale, maxScale, distance / maxDistance);

            foreach (var waveDir in waveDirs)
            {
                Vector2 spawnPos = (Vector2)firePoint.position + waveDir.normalized * distance;

                GameObject pillar = PoolManager.Instance.Get(icePillarPrefab);
                pillar.transform.position = spawnPos;

                pillar.transform.localScale = Vector3.one * scale;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}
