using UnityEngine;
using UnityEngine.UIElements;

public enum WallSkillStatTypes { MaxHP, Duration}
public class PlayerWallSkill : Skill
{
    [Header("벽 정보")]
    [SerializeField] private float initial_maxHP;
    [SerializeField] private float initial_duration;

    private PlayerStat maxHP;
    private PlayerStat duration;

    public float MaxHP => maxHP.GetValue();
    public float Duration => duration.GetValue();


    public override bool TryPreviewSkill()
    {
        return base.TryPreviewSkill();
    }

    protected override void Start()
    {
        base.Start();
        canBeFlipX = true;

        InitializeStats();
    }

    private void InitializeStats()
    {
        maxHP = new PlayerStat(initial_maxHP);
        duration = new PlayerStat(initial_duration);
    }

    protected override void UseSkill()
    {
        base.UseSkill();
        Debug.Log("벽스킬");
        var go = PoolManager.Instance.Get(skillPrefab);
        var wallScript = go.GetComponent<WallSkillController>();
        wallScript.SetWall(skillCenterPosition, MaxHP, Duration, isDirectionSE);
    }

    public void AddModifier(WallSkillStatTypes type, PlayerStatModifier modifier)
    {
        PlayerStat targetStat = GetTargetStat(type);

        targetStat?.AddModifier(modifier);
    }

    public void RemoveModifier(WallSkillStatTypes type, PlayerStatModifier modifier)
    {
        PlayerStat targetStat = GetTargetStat(type);

        targetStat?.RemoveModifier(modifier);
    }
    private PlayerStat GetTargetStat(WallSkillStatTypes type)
    {
        return type switch
        {
            WallSkillStatTypes.MaxHP => maxHP,
            WallSkillStatTypes.Duration => duration,
            _ => null
        };
    }
}
