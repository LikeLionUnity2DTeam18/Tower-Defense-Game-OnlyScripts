using UnityEngine;

/// <summary>
/// 구속의 사격 이펙트 및 데미지 판정을 위한 오브젝트
/// </summary>
public class BindShotController : PlayerSkillEntity
{
    private float damage;
    private float bindTime;
    private Vector2 hitboxSize = new Vector2(2.4f, 1.4f);
    private float hitboxRotationAngle;
    private Direction4Custom direction;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    /// <summary>
    /// 초기화
    /// </summary>
    /// <param name="position"></param>
    /// <param name="damage"></param>
    /// <param name="bindTime"></param>
    /// <param name="dir"></param>
    public void SetBindShot(Vector2 position, float damage, float bindTime, Direction4Custom dir)
    {
        transform.position = position;
        bool isHaveDuration = false;
        SetEntity(isHaveDuration);
        this.damage = damage;
        this.bindTime = bindTime;
        direction = dir;

        SetDirection();

        anim.SetTrigger("Trigger");
    }


    /// <summary>
    /// 스킬 방향에 따른 스프라이트 방향 및 공격범위 회전각도 설정
    /// </summary>
    private void SetDirection()
    {
        switch (direction)
        {
            case Direction4Custom.SE:
                hitboxRotationAngle = 45f;
                break;
            case Direction4Custom.SW:
                hitboxRotationAngle = 45f;
                FlipX();
                break;
            case Direction4Custom.NE:
                hitboxRotationAngle = -45f;
                break;
            case Direction4Custom.NW:
                hitboxRotationAngle = -45f;
                FlipX();
                break;
        }
    }

    /// <summary>
    /// 애니매이션 끝에 호출해서 해당위치 적들 멈추고 대미지
    /// </summary>
    public override void SkillHitOnAnimation()
    {
        var hits = Physics2D.OverlapBoxAll(transform.position, hitboxSize, hitboxRotationAngle, LayerMask.GetMask("Enemy"));
        if (hits.Length < 0)
            return;

        foreach (var hit in hits)
        {
            Debug.Log("스킬 히트");
            hit.GetComponent<EnemyController>().TakeDamage(damage);
            //hit.GetComponent<EnemyController>().Bind(bindTime);
        }

    }


    /// <summary>
    /// 회전시킨 기즈모 그리기 by 지피티
    /// </summary>
    private void OnDrawGizmos()
    {
        // 회전 행렬 설정
        Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, Quaternion.Euler(0, 0, hitboxRotationAngle), Vector3.one);

        // 기존 Gizmos 매트릭스 저장
        Matrix4x4 oldGizmosMatrix = Gizmos.matrix;

        // Gizmos 매트릭스 교체
        Gizmos.matrix = rotationMatrix;

        // 빨간색으로 회전된 박스 그리기
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.zero, hitboxSize);

        // 매트릭스 원복
        Gizmos.matrix = oldGizmosMatrix;
    }
}
