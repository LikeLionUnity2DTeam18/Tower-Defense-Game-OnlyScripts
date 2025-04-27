using UnityEngine;

/// <summary>
/// 대상을 향해 유도되는 투사체
/// 투사체 공통 부모인 PlayerProjectile을 상속해서 Update는 base 사용하지 않고 재정의
/// </summary>
public class PlayerHoamingProjectile : PlayerProjectile
{
    protected EnemyController target;
    protected Vector2 destination;

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

        HoamingCollisionCheck();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        // 유도 투사체는 충돌체크 필요없음
    }

    protected virtual void HoamingCollisionCheck()
    {
        if (IsSamePosition(destination, (Vector2)transform.position))
        {

            target?.TakeDamage(1);
            Release();
        }
    }
}
