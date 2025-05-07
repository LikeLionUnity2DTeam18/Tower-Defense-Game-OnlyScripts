using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public enum PowerUpSkillStatTypes { damageUpAmount, duration, aoe}

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

    protected override void UseSkill()
    {
        base.UseSkill();
        var go = PoolManager.Instance.Get(skillPrefab);
        var obj = go.GetComponent<PowerUpController>();
        obj.SetPowerUp(player.transform.position, DamageUpAmount, Duration, Aoe);
    }


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
        sb.AppendLine($"<b>타워 강화</b>");
        sb.AppendLine($"쿨타임: {cooldown}초");
        sb.AppendLine(); // 빈 줄
        // 스킬 스탯
        sb.AppendLine($"타워 공격력 상승: {DamageUpAmount}");
        sb.AppendLine($"지속시간: {Duration}초");

        tooltipText = sb.ToString();
    }
}
