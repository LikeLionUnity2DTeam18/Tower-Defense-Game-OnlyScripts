using Unity.VisualScripting;
using UnityEngine;

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


    public void AddModifier(PowerUpSkillStatTypes type, PlayerStatModifier modifier)
    {
        PlayerStat targetStat = GetTargetStat(type);

        targetStat?.AddModifier(modifier);
    }

    public void RemoveModifier(PowerUpSkillStatTypes type, PlayerStatModifier modifier)
    {
        PlayerStat targetStat = GetTargetStat(type);

        targetStat?.RemoveModifier(modifier);
    }
    private PlayerStat GetTargetStat(PowerUpSkillStatTypes type)
    {
        return type switch
        {
            PowerUpSkillStatTypes.damageUpAmount => damageUpAmount,
            PowerUpSkillStatTypes.duration => duration,
            PowerUpSkillStatTypes.aoe => aoe,
            _ => null
        };
    }
}
