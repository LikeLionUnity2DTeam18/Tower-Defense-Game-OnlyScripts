using DG.Tweening;
using System.Collections;
using UnityEngine;

public class DeerGod_flower : TowerStay
{
    Animator anim;
    [Header("투사체")]
    [SerializeField] private GameObject projectile;
    public float interval = 0.1f;
    public float distanceBetweenSpikes = 1f;
    public float repeatTime = 3f;
    public float distance = 5f;
    private GameObject targetEnemy;

    private void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(RepeatProjectile());
    }

    protected override void Update()
    {
        base.Update();
        targetEnemy = FindNearestEnemyByOverlap(transform.position, distance, LayerMask.GetMask("Enemy"));
    }

    protected override void DurationEnd()
    {
        if (timer <= 0)
        {
            anim.SetBool("Destroy", true);
        }
    }

    private IEnumerator RepeatProjectile()
    {
        while (true)
        {
            if (targetEnemy != null)
            {
                Vector2 startPos = transform.position;
                Vector2 targetPos = targetEnemy.transform.position;

                StartCoroutine(SpawnSpikesRoutine(startPos, targetPos));
            }

            yield return new WaitForSeconds(repeatTime);
        }
    }
    public void StartProjectile(Vector2 startPos, Vector2 targetPos)
    {
        if (targetPos != null) StartCoroutine(SpawnSpikesRoutine(startPos, targetPos));
    }

    private IEnumerator SpawnSpikesRoutine(Vector2 startPos, Vector2 targetPos)
    {
        Vector2 dir = (targetPos - startPos).normalized;
        float totalDistance = Vector2.Distance(startPos, targetPos);
        int spikeCount = Mathf.FloorToInt(totalDistance / distanceBetweenSpikes);
        for (int i = 1; i <= spikeCount; i++)
        {
            Vector2 spawnPos = startPos + dir * distanceBetweenSpikes * i;
            GameObject spike = tower.SpawnWithStats(projectile);
            spike.transform.position = spawnPos;
            spike.transform.rotation = Quaternion.identity;

            yield return new WaitForSeconds(interval);
        }
    }

    public void FlowerDestroy()
    {
        PoolManager.Instance.Return(gameObject);
        timer = duration;
        transform.DOKill();
    }
}
