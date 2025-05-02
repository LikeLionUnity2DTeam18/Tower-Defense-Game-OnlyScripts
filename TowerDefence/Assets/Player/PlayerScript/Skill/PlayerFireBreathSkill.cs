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
        player.stateMachine.ChangeState(player.breathState);
    }

    private void CreateSkillObject()
    {
        var go = PoolManager.Instance.Get(skillPrefab);
        var obj = go.GetComponent<FireBreathController>();
        obj.SetFireBreath(skillCenterPosition, duration.GetValue(), damage.GetValue(), damageInterval.GetValue(), length.GetValue());
    }

    public void AddModifier(FireBreathStatTypes type, PlayerStatModifier modifier)
    {
        PlayerStat targetStat = GetTargetStat(type);

        targetStat?.AddModifier(modifier);
    }



    public void RemoveModifier(FireBreathStatTypes type, PlayerStatModifier modifier)
    {
        PlayerStat targetStat = GetTargetStat(type);

        targetStat?.RemoveModifier(modifier);
    }
    private PlayerStat GetTargetStat(FireBreathStatTypes type)
    {
        return type switch
        {
            FireBreathStatTypes.Damage => damage,
            FireBreathStatTypes.DamageInterval => damageInterval,
            FireBreathStatTypes.Duration => duration,
            FireBreathStatTypes.Length => length,
            _ => null
        };
    }
}
