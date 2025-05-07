using System.Text;
using UnityEngine;




/// <summary>
/// 타워 강화 스킬
/// </summary>
public class PlayerPowerUpSkill : Skill
{

    [Header("강화 정보")]
    [SerializeField] private int initial_damageUpAmount;  // 공격력 버프 수치
    [SerializeField] private float initial_duration;
    [SerializeField] private float initial_Aoe;

    private PlayerStat damageUpAmount;
    private PlayerStat duration;
    private PlayerStat aoe;

    public int DamageUpAmount => (int) damageUpAmount.GetValue();
    public float Duration => duration.GetValue();
    public float Aoe => aoe.GetValue();

    protected override void Start()
    {
        base.Start();

        InitializeStats();
    }

    protected override void Update()
    {
        if (cooldownTimer > 0)
            cooldownTimer -= Time.deltaTime;

        
        UpdateSkillRangeDisplay();

    }

    private void InitializeStats()
    {
        damageUpAmount = new PlayerStat(initial_damageUpAmount);
        duration = new PlayerStat(initial_duration);
        aoe = new PlayerStat(initial_Aoe);
    }

    /// <summary>
    /// 미리보기 상태는 있는데 미리보기 프리펩은 없는 특이한 스킬이라 메서드 재정의
    /// 지금 생각해보면 부모 메서드에서 preview 널체크로 어떻게 하면 됐을것같음..
    /// </summary>
    /// <returns></returns>
    public override bool TryPreviewSkill()
    {
        if (CanUseSkill() && !isPreviewState)
        {
            isPreviewState = true;
            skillRange = Aoe;
            rangeScript = PoolManager.Instance.Get(rangePrefab).GetComponent<PlayerSkillRangeController>();
            rangeScript.SetSkillRangeDisplay(player.transform.position, skillRange);

            return true;
        }
        return false;
    }

    /// <summary>
    /// /// 미리보기 상태는 있는데 미리보기 프리펩은 없는 특이한 스킬이라 메서드 재정의
    /// </summary>
    protected override void UseSkill()
    {
        base.UseSkill();
        var go = PoolManager.Instance.Get(skillPrefab);
        var obj = go.GetComponent<PowerUpController>();
        obj.SetPowerUp(player.transform.position, DamageUpAmount, Duration, Aoe);
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
            PlayerStatTypes.PowerupDamageUpAmount => damageUpAmount,
            PlayerStatTypes.PowerupDuration => duration,
            PlayerStatTypes.PowerupAoe => aoe,
            _ => null
        };
    }

    /// <summary>
    /// 툴팁 텍스트 get
    /// </summary>
    /// <returns></returns
    public override string GetTooltipText()
    {
        if (tooltipText == null)
            SetTooltipText();
        return tooltipText;
    }

    /// <summary>
    ///  StringBuilder를 이용해 툴팁 문자열 생성
    /// </summary>
    public override void SetTooltipText()
    {

        var sb = new StringBuilder();

        // 스킬 이름
        sb.AppendLine($"<b>타워 강화</b>");
        sb.AppendLine($"쿨타임: {cooldown}초");
        sb.AppendLine(); // 빈 줄
        // 스킬 스탯
        sb.AppendLine($"타워 공격력 상승: {DamageUpAmount}");
        sb.AppendLine($"지속시간: {Duration}초");

        tooltipText = sb.ToString();
    }
}
