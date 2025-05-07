using UnityEngine;

/// <summary>
/// 대상을 향해 유도되는 투사체
/// 투사체 공통 부모인 PlayerProjectile을 상속해서 Update는 base 사용하지 않고 재정의
/// </summary>
public class PlayerHoamingProjectile : PlayerProjectile
{
    protected EnemyController target;
    protected Vector2 destination;
    protected float changeTargetRange;

    protected override void Awake()
    {
        base.Awake();
        isHoaming = true;
    }

    public virtual void Initialize(Vector2 _position, EnemyController _target)
    {
        transform.position = _position;
        target = _target;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        travelTimer = 999; // 유도투사체는 적위치에 도착 할 때까지 사라지지 않음
    }

    protected override void Update()
    {
        if (target != null)
        {
            destination = target.transform.position;
        }

        direction = destination - (Vector2)transform.position;
        RotateToMovingDirection();
        rb.linearVelocity = direction.normalized * speed;

        FindAnotherTarget();
        HoamingCollisionCheck();
        
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        // 유도 투사체는 충돌체크 필요없음
    }

    /// <summary>
    /// 
    /// </summary>
    protected virtual void HoamingCollisionCheck()
    {
        if (IsSamePosition(destination, (Vector2)transform.position))
        {

            target?.TakeDamage(damage);
            Release();
        }
    }

    protected virtual void FindAnotherTarget()
    {
        // 타겟이 비활성화 된 경우 새 타겟 찾기
        if (target.gameObject.activeInHierarchy)
        {
            target = FindTargetInRange(changeTargetRange);
        }
    }

    /// <summary>
    /// range 안의 가장 가까운 몬스터 대상을 반환
    /// </summary>
    /// <returns></returns>
    protected virtual EnemyController FindTargetInRange(float _range)
    {
        var targets = Physics2D.OverlapCircleAll(transform.position, _range, LayerMask.GetMask("Enemy"));
        if (targets.Length <= 0)
        {
            Debug.Log("대상 없음!");
            return null;
        }
        float nearestDistance = Mathf.Infinity;
        int nearestTargetIndex = 0;
        EnemyController nearestTarget;
        for (int i = 0; i < targets.Length; i++)
        {
            float currentdistance = Vector3.Distance(targets[i].transform.position, transform.position);
            if (nearestDistance < currentdistance)
            {
                nearestDistance = currentdistance;
                nearestTargetIndex = i;
            }
        }
        nearestTarget = targets[nearestTargetIndex].gameObject.GetComponent<EnemyController>();
        return nearestTarget;
    }
}
