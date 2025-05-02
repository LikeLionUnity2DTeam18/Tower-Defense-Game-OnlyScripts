using UnityEngine;

public class TowerGuidance : TowerEntity
{
    private GameObject target;
    private float speed = 3f;
    private float distance = 5f;
    public override void Update()
    {
        base.Update();
        target = FindNearestEnemyByOverlap(transform.position, distance, LayerMask.GetMask("Enemy"));
        transform.Translate(Dir() * speed * Time.deltaTime);
    }

    private Vector3 Dir()
    {
        if(target == null) return Vector3.zero;
        Vector3 dir = target.transform.position - transform.position;
        return dir;
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
