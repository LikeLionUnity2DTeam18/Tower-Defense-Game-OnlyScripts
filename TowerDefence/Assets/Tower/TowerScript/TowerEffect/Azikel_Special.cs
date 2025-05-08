using UnityEngine;
using static UnityEngine.ParticleSystem;


public class Azikel_Special : TowerProjectile
{
    [SerializeField] private float rangeRadius = 5f;
    [SerializeField] private LayerMask enemyLayerMask;
    [SerializeField] private ParticleSystem particle;

    private GameObject currentTarget;

    public override void Update()
    {
        if (particle != null)
        {
            MainModule main = particle.main;
            float playTime = particle.time;
            float lifetime = main.startLifetime.constant;
            float duration = main.duration;

            if (playTime < duration - lifetime)
                return;
        }

        // 유도: 타겟이 살아있다면 계속 방향 갱신
        if (currentTarget != null)
        {
            direction = (currentTarget.transform.position - transform.position).normalized;
        }
        else
        {
            // 타겟이 없으면 새로 찾음
            currentTarget = FindClosestEnemyExcluding(null);
            if (currentTarget == null)
            {
                isReady = true;
            }
        }

        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        if (isReady)
        {
            isReady = false;
            PoolManager.Instance.Return(gameObject);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Enemy")) return;

        if (collision.TryGetComponent<EnemyController>(out EnemyController targetStats))
        {
            stats?.DoSpecialDamage(targetStats);
        }

        // 현재 타겟 유지
        if (currentTarget == null)
            currentTarget = collision.gameObject;
    }

    GameObject FindClosestEnemyExcluding(GameObject excludedEnemy)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, rangeRadius, enemyLayerMask);

        GameObject closest = null;
        float minDistSqr = rangeRadius * rangeRadius;
        Vector2 currentPos = transform.position;

        foreach (var hit in hits)
        {
            GameObject enemy = hit.gameObject;
            if (enemy == excludedEnemy) continue;
            if (enemy == null) continue;

            float distSqr = ((Vector2)enemy.transform.position - currentPos).sqrMagnitude;
            if (distSqr < minDistSqr)
            {
                minDistSqr = distSqr;
                closest = enemy;
            }
        }

        return closest;
    }
}