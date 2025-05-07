using System.Text;
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


    public override PlayerStat GetStatByType(PlayerStatTypes type)
    {
        return type switch
        {
            PlayerStatTypes.WallMaxHP => maxHP,
            PlayerStatTypes.WallDuration => duration,
            _ => null
        };
    }
    public override string GetTooltipText()
    {
        if (tooltipText == null)
            SetTooltipText();
        return tooltipText;
    }

    public override void SetTooltipText()
    {

        var sb = new StringBuilder();

        // 스킬 이름
        sb.AppendLine($"<b>벽 생성</b>");
        sb.AppendLine($"쿨타임: {cooldown}초");
        sb.AppendLine(); // 빈 줄
        // 스킬 스탯
        sb.AppendLine($"벽 체력: {MaxHP}");
        sb.AppendLine($"지속시간: {Duration}초");

        tooltipText = sb.ToString();
    }
}
