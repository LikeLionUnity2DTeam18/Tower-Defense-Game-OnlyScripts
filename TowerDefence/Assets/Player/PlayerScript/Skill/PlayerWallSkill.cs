using System.Text;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// 벽 생성 스킬
/// </summary>
public class PlayerWallSkill : Skill
{
    [Header("벽 정보")]
    [SerializeField] private float initial_maxHP;
    [SerializeField] private float initial_duration;

    private PlayerStat maxHP;
    private PlayerStat duration;

    public float MaxHP => maxHP.GetValue();
    public float Duration => duration.GetValue();



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

    /// <summary>
    /// 플레이어 스탯 타입으로 스탯 return
    /// 다음에는 skillmanager에서 관리할수있게 해야겠음..
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public override PlayerStat GetStatByType(PlayerStatTypes type)
    {
        return type switch
        {
            PlayerStatTypes.WallMaxHP => maxHP,
            PlayerStatTypes.WallDuration => duration,
            _ => null
        };
    }
    /// <summary>
    /// 툴팁 텍스트 get
    /// </summary>
    /// <returns></returns>
    public override string GetTooltipText()
    {
        if (tooltipText == null)
            SetTooltipText();
        return tooltipText;
    }

    /// <summary>
    /// StringBuilder를 이용해 툴팁 문자열 생성
    /// </summary>
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
