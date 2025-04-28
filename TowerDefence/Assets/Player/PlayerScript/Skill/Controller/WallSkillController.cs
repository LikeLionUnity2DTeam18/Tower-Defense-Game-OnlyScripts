using UnityEngine;

public class WallSkillController : PlayerSkillEntity
{
    private SpriteRenderer sr;
    public float HP { get; private set; }
    public bool isDirectionSE { get; private set; }



    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }


    public void SetWall(Vector2 position, float HP, float duration, bool isDirectionSE)
    {
        Debug.Log($"벽 생성됨, {position}, hp {HP} duration {duration}");
        SetEntity();
        transform.position = position;
        this.HP = HP;
        this.durationTimer = duration;
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
