public class PlayerStatManager
{
    public PlayerStat baseAttack;
    public PlayerStat moveSpeed;
    public PlayerStat baseattackSpeed;
    public PlayerStat baseattackRange;

    public PlayerStatManager(PlayerStatsSO baseStat)
    {
        baseAttack = CreateStat(PlayerStatTypes.baseAttackDamage, baseStat.baseAttackDamage);
        moveSpeed = CreateStat(PlayerStatTypes.moveSpeed, baseStat.moveSpeed);
        baseattackSpeed = CreateStat(PlayerStatTypes.baseattackSpeed, baseStat.baseattackSpeed);
        baseattackRange = CreateStat(PlayerStatTypes.baseattackRange, baseStat.baseattackRange);
    }

    private PlayerStat CreateStat(PlayerStatTypes _type, float value)
    {
        PlayerStat newStat = new PlayerStat(value);
        newStat.OnValueChanged = () => EventManager.Trigger(new PlayerStatChanged(_type, newStat.GetValue()));
        return newStat;
    }
}
