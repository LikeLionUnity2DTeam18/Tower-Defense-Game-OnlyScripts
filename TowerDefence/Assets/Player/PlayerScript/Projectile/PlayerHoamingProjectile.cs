using UnityEngine;

/// <summary>
/// 대상을 향해 유도되는 투사체
/// 투사체 공통 부모인 PlayerProjectile을 상속해서 Update는 base 사용하지 않고 재정의
/// </summary>
public class PlayerHoamingProjectile : PlayerProjectile
{
    protected Transform target;
    protected Vector2 destination;

    protected override void Awake()
    {
        base.Awake();
    }

    public virtual void Initialize(Transform _target)
    {
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
            destination = target.position;
        }

        direction = destination - (Vector2)transform.position;
        RotateToMovingDirection();
        rb.linearVelocity = direction.normalized * speed;

        if(IsSamePosition(destination, (Vector2)transform.position))
        {
            // 풀로 리턴
            Debug.Log($"명중! {damage}의 데미지");
        }
    }
}
