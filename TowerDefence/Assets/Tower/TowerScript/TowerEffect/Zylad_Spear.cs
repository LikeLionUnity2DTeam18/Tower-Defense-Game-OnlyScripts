using System.Collections;
using UnityEngine;

public class Zylad_Spear : TowerEntity
{
    [SerializeField] private GameObject spear;
    [SerializeField] private GameObject splashEffectPrefab;

    private void OnEnable()
    {
        StartCoroutine(DelayedDetectAndFire());
    }

    private IEnumerator DelayedDetectAndFire()
    {
        yield return new WaitForSeconds(3f); // 1초 대기

        Transform nearest = FindNearestEnemy(transform.position, 10f);
        if (nearest != null)
        {
            GameObject t = SpawnWithStats(spear);
            t.transform.position = nearest.position;

            GameObject splashEffect = PoolManager.Instance.Get(splashEffectPrefab);
            splashEffect.transform.position = nearest.position;
            splashEffect.transform.localScale = splashEffect.transform.localScale / 4f;
        }

        PoolManager.Instance.Return(gameObject); // 본인 반환
    }

    private GameObject SpawnWithStats(GameObject prefab)
    {
        GameObject t = PoolManager.Instance.Get(prefab);
        var stuck = t.GetComponent<Zylad_SpearStuck>();
        stuck.stats = stats;
        return t;
    }

    Transform FindNearestEnemy(Vector3 origin, float radius)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(origin, radius, LayerMask.GetMask("Enemy"));
        Transform nearest = null;
        float minDist = Mathf.Infinity;

        foreach (var hit in hits)
        {
            float dist = Vector2.SqrMagnitude(hit.transform.position - origin); // 성능 위해 제곱 거리
            if (dist < minDist)
            {
                minDist = dist;
                nearest = hit.transform;
            }
        }

        return nearest;
    }
}
