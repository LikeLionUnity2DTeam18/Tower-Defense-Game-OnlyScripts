using DG.Tweening;
using UnityEngine;

public class TowerStay : MonoBehaviour
{
    [SerializeField] protected float duration = 10f;
    [SerializeField] protected float timer = 10f;

    protected virtual void Update()
    {
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

    public void ResetProjectile()
    {
        PoolManager.Instance.Return(gameObject);
        timer = duration;
        transform.DOKill();
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
}
