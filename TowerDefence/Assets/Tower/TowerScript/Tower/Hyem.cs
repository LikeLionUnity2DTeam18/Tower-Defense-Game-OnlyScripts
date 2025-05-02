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
    private static readonly float[] angles = { -15f, 0f, 15f }; // 3개 spreadAngle = 30
    private static readonly float[] distances = { 1f, 3f, 5f}; // 4개 maxDistance = 6
    private static readonly float[] scales = { 1f, 2f, 3f }; // 1~3 구간 비례
    private static readonly WaitForSeconds shortWait = new WaitForSeconds(0.1f);
    private WaitForSeconds delayWait;
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
        delayWait = new WaitForSeconds(delayBetweenShots);
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
            GameObject spear = SpawnWithStats(projectile);
            spear.transform.position = point.position;
            spear.GetComponent<TowerProjectile>().Init(dir);

            yield return delayWait;
        }
    }


    public void CastIceCone()
    {
        StartCoroutine(IceConeCoroutine());
    }

    private IEnumerator IceConeCoroutine()
    {
        Vector2 baseDir = dir;

        Vector2[] waveDirs = new Vector2[3];
        for (int i = 0; i < 3; i++)
        {
            waveDirs[i] = Quaternion.Euler(0, 0, angles[i]) * baseDir;
        }

        for (int j = 0; j < 3; j++)
        {
            float dist = distances[j];
            float scale = scales[j];

            foreach (var waveDir in waveDirs)
            {
                Vector2 spawnPos = (Vector2)firePoint.position + waveDir * dist;
                GameObject pillar = SpawnWithStats(icePillarPrefab);
                pillar.transform.position = spawnPos;
                pillar.transform.localScale = Vector3.one * scale;
            }

            yield return shortWait;
        }
    }

}
