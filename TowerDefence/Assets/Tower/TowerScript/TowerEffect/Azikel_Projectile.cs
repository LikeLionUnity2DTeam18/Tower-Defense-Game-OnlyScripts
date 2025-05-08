using UnityEngine;


public class Azikel_Projectile : TowerProjectile
{
    [SerializeField] private float rangeRadius = 5f;
    [SerializeField] private LayerMask enemyLayerMask;
    public override void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        if (isReady)
        {
            isReady = false;
            PoolManager.Instance.Return(gameObject);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collision.TryGetComponent<EnemyController>(out EnemyController targetStats);
            stats?.DoRangeDamage(targetStats);

            GameObject newTarget = FindClosestEnemyExcluding(collision.gameObject);
            if (newTarget != null)
            {
                //방향
                direction = (newTarget.transform.position - transform.position).normalized;
                //회전
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            else
            {
                isReady = true; // 더 이상 적이 없으면 제거
            }
        }
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
