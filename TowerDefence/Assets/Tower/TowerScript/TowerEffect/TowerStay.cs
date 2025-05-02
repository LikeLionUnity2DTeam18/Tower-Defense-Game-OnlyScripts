using DG.Tweening;
using UnityEngine;

public class TowerStay : TowerEntity
{
    [SerializeField] protected float duration = 10f;
    [SerializeField] protected float timer = 10f;

    public override void Update()
    {
        base.Update();
        Timer();
        DurationEnd();
    }
    private void Timer()
    {
        if (timer > 0) timer -= Time.deltaTime;
    }
    protected virtual void DurationEnd()
    {
        if (timer <= 0)
        {
            ResetProjectile();
        }
    }

    public virtual void  ResetProjectile()
    {
        DOTween.Kill(this);
        PoolManager.Instance.Return(gameObject);
        timer = duration;
    }

    //가까운 적 찾기
    public GameObject FindNearestEnemyByOverlap(Vector3 origin, float radius, LayerMask enemyLayer)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(origin, radius, enemyLayer);
        if (hits.Length == 0) return null;

        GameObject nearest = null;
        float minDist = float.MaxValue;

        foreach (var hit in hits)
        {
            float dist = (hit.transform.position - origin).sqrMagnitude;

            if (dist < minDist)
            {
                minDist = dist;
                nearest = hit.gameObject;
            }
        }
        return nearest;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        // Enemy 레이어만 통과
        if (collision.gameObject.layer != LayerMask.NameToLayer("Enemy")) return;

        // 타겟 스탯 가져오기
        collision.TryGetComponent<TowerStats>(out TowerStats targetStats);

        // 내 스탯 기준으로 데미지 주기
        stats?.DoSpecialDamage(targetStats);
    }
}
