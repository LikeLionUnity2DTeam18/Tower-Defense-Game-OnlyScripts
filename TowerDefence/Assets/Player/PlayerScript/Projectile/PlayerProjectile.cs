using UnityEngine;

/// <summary>
/// 플레이어의 투사체들이 공통으로 상속할 클래스
/// 주어진 방향, 속도로 이동
/// 몬스터와 충돌 시 데미지 처리
/// 화면 밖으로 사라질 시 오브젝트풀 return
/// </summary>
public class PlayerProjectile : MonoBehaviour
{
    // 컴포넌트
    protected Rigidbody2D rb;


    [Header("투사체 기본 정보")]
    [SerializeField] protected Vector2 direction;
    [SerializeField] protected float speed;
    [SerializeField] protected float travelTime;
    protected float travelTimer;
    

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void OnEnable()
    {
        travelTimer = travelTime;
    }

    protected virtual void Update()
    {
        travelTimer -= Time.deltaTime;

        if (travelTimer < 0)
        {
            // 풀로 리턴
        }

        rb.linearVelocity = direction.normalized * speed;
        RotateToMovingDirection();
    }


    // 투사체 생성 후 초기화
    public virtual void Initialize(Vector2 _direction)
    {
        direction = _direction;
    }

    // 투사체 진행 방향으로 Transform을 회전해서 이미지 보정
    protected virtual void RotateToMovingDirection()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle );
    }

    public bool IsSamePosition(Vector2 a, Vector2 b) => Vector2.Distance(a, b) < 0.1f;
}
