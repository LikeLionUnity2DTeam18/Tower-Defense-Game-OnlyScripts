using System.Collections;
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
    protected float damage;

    protected bool isReleased = false; // 풀로 리턴됐는지 여부를 체크
    protected bool isHoaming = false; // 유도미사일은 콜라이더 트리거체크 안함
    private int enemyLayer;

    Coroutine releaseCouritine;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyLayer = LayerMask.NameToLayer("Enemy");
    }

    protected virtual void OnEnable()
    {
        travelTimer = travelTime;
        isReleased = false;
    }

    protected virtual void OnDisable()
    {
        if (releaseCouritine != null)
        {
            StopCoroutine(releaseCouritine);
            releaseCouritine = null;
        }
    }

    protected virtual void Update()
    {
        travelTimer -= Time.deltaTime;

        if (travelTimer < 0)
        {
            Release();
        }

        rb.linearVelocity = direction.normalized * speed;
        RotateToMovingDirection();
    }


    // 투사체 생성 후 초기화
    public virtual void Initialize(Vector2 _direction, float _damage)
    {
        direction = _direction;
        damage = _damage;
    }

    // 투사체 진행 방향으로 Transform을 회전해서 이미지 보정
    protected virtual void RotateToMovingDirection()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public bool IsSamePosition(Vector2 a, Vector2 b) => Vector2.Distance(a, b) < 0.2f;

    /// <summary>
    /// 오브젝트 풀로 리턴, 풀로 리턴할 때 OnBecameInvisible가 중복호출 되는 경우가 있어서 중복호출 체크
    /// </summary>
    public void Release()
    {
        if (!isReleased)
        {
            isReleased = true;
            PoolManager.Instance.Return(gameObject);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (isHoaming) // 유도 투사체는 콜라이더체크 안함
            return;

        if (collision.gameObject.layer != enemyLayer)
            return;

        var target = collision.gameObject.GetComponent<EnemyController>();
        if (target != null)
        {
            target.TakeDamage(1);
            Release();
        }
    }

    protected virtual void OnBecameInvisible()
    {
        if (gameObject.activeInHierarchy)
            releaseCouritine = StartCoroutine(ReleaseAfterSeconds(3f));
    }

    protected virtual void OnBecameVisible()
    {
        if (releaseCouritine != null)
        {
            StopCoroutine(releaseCouritine);
            releaseCouritine = null;
        }
    }




    IEnumerator ReleaseAfterSeconds(float _seconds)
    {
        yield return new WaitForSeconds(_seconds);
        Release();
    }
}
