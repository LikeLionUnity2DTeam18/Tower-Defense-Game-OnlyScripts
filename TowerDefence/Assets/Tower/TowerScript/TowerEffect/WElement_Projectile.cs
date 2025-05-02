using UnityEngine;

public class WElement_Projectile : TowerProjectile
{
    public float yGrowSpeed = 1f;
    private Vector3 initialScale;

    void Awake()
    {
        initialScale = transform.localScale;  // 원래 크기 저장
    }

    void OnEnable()
    {
        transform.localScale = initialScale;  // 재활성화 시 크기 리셋
    }

    public override void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        // y축으로 점진적 확대
        Vector3 scale = transform.localScale;
        scale.y += yGrowSpeed * Time.deltaTime;
        transform.localScale = scale;

        if (Vector3.Distance(startPos, transform.position) > maxDistance)
        {
            isReady = false;

            // 반납 직전에 크기 초기화
            transform.localScale = initialScale;
            PoolManager.Instance.Return(gameObject);
            transform.position = startPos;  // 원래 위치로 되돌리기
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Enemy")) return;
        collision.TryGetComponent<TowerStats>(out TowerStats targetStats);
        stats?.DoRangeDamage(targetStats);
    }
}
