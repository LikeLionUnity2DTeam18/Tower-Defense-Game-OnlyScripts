using UnityEngine;
using static UnityEngine.Rendering.ProbeAdjustmentVolume;

public class FElement_OnOff : TowerOnOff
{
    ElementFire element;
    [SerializeField] private ParticleSystem ps; // 파티클 시스템 연결
    private ParticleSystem.ShapeModule shape;
    //크기조절
    private Vector3 initialScale;
    [SerializeField] private float scaleFactor = 1f;

    //데미지 간격
    [SerializeField] private float damageInterval = 0.5f;  // 0.5초마다
    private float damageTimer;

    public override void Awake()
    {
        base.Awake();
        element = GetComponentInParent<ElementFire>();
        initialScale = transform.localScale;

        if (ps != null)
            shape = ps.shape;
    }

    public override void Update()
    {
        var target = element?.nearestREnemy;
        if (target == null)
        {
            if (ps != null && ps.isPlaying)
                ps.Stop();
                ps.Clear();

            return;
        }

        // 타겟 있으면 재생
        if (ps != null && !ps.isPlaying)
            ps.Play();

        //회전
        Vector2 dir = (target.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //거리 따라 x축 크기 변화  
        float dist = Vector2.Distance(transform.position, target.transform.position);
        Vector3 s = initialScale;
        s.x = dist * scaleFactor; 
        transform.localScale = s;

        damageTimer += Time.deltaTime;

        if (ps != null)
        {
            ps.transform.position = transform.position;
            ps.transform.rotation = transform.rotation;
            shape.scale = new Vector3(s.x*2.5f, shape.scale.y, shape.scale.z);
            shape.position = new Vector3(s.x * 1.5f, 0f, 0f);
        }
    }

    protected void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Enemy")) return;

        if (damageTimer >= damageInterval && collision.TryGetComponent<TowerStats>(out TowerStats targetStats))
        {
            stats?.DoRangeDamage(targetStats);
            damageTimer = 0f;
        }
    }
}
