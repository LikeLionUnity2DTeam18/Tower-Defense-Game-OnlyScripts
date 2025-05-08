using UnityEngine;

/// <summary>
/// 벽 생성 스킬
/// </summary>
public class WallSkillController : PlayerSkillEntity
{
    private SpriteRenderer sr;
    public float HP { get; private set; }
    public bool isDirectionSE { get; private set; }



    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    /// <summary>
    /// 초기화
    /// </summary>
    /// <param name="position"></param>
    /// <param name="HP"></param>
    /// <param name="duration"></param>
    /// <param name="isDirectionSE"></param>
    public void SetWall(Vector2 position, float HP, float duration, bool isDirectionSE)
    {
        Debug.Log($"벽 생성됨, {position}, hp {HP} duration {duration}");

        transform.position = position;
        this.HP = HP;
        this.durationTimer = duration;
        SetEntity(true);
        this.isDirectionSE = isDirectionSE;
        if (!isDirectionSE)
        {
            FlipX();
        }
    }



    public void TakeDamage(float dmg)
    {
        HP -= dmg;
        if (HP <= 0)
            Release();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

    }
}
