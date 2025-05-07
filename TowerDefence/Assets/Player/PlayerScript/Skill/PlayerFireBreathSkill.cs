using System.Text;
using UnityEngine;

public enum FireBreathStatTypes { Duration, Damage, DamageInterval, Length}

public class PlayerFireBreathSkill : Skill
{
    [SerializeField] float initial_duration;
    [SerializeField] float initial_damage;
    [SerializeField] float initial_damageInterval;
    [SerializeField] float initial_length;

    private PlayerStat duration;
    private PlayerStat damage;
    private PlayerStat damageInterval;
    private PlayerStat length;

    float Duration => duration.GetValue();
    float Damage => damage.GetValue();
    float DamageInterval => damageInterval.GetValue();
    float Length => length.GetValue();

    protected override void Start()
    {
        base.Start();
        hasPreviewState = false;

        InitializeStats();
    }

    private void InitializeStats()
    {
        duration = new PlayerStat(initial_duration);
        damage = new PlayerStat(initial_damage);
        damageInterval = new PlayerStat(initial_damageInterval);
        length = new PlayerStat(initial_length);
    }

    protected override void UseSkill()
    {
        base.UseSkill();
        CreateSkillObject();
        player.StateMachine.ChangeState(player.BreathState);
    }

    private void CreateSkillObject()
    {
        var go = PoolManager.Instance.Get(skillPrefab);
        var obj = go.GetComponent<FireBreathController>();
        obj.SetFireBreath(skillCenterPosition, duration.GetValue(), damage.GetValue(), damageInterval.GetValue(), length.GetValue());
    }


    public override PlayerStat GetStatByType(PlayerStatTypes type)
    {
        return type switch
        {
            PlayerStatTypes.FireBreathDamage => damage,
            PlayerStatTypes.FireBreathDamageInterval => damageInterval,
            PlayerStatTypes.FireBreathDuration => duration,
            PlayerStatTypes.FireBreathLength => length,
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
        sb.AppendLine($"<b>화염 숨결</b>");
        sb.AppendLine($"쿨타임: {cooldown}초");
        sb.AppendLine(); // 빈 줄
        // 스킬 스탯
        sb.AppendLine($"틱당 데미지: {Damage}");
        sb.AppendLine($"데미지 간격: {DamageInterval}초");
        sb.AppendLine($"지속 시간: {Duration}초");

        tooltipText = sb.ToString();
    }
}
