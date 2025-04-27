public class PlayerStatManager
{
    public PlayerStat baseAttack;
    public PlayerStat moveSpeed;
    public PlayerStat baseattackSpeed;
    public PlayerStat baseattackRange;

    public PlayerStatManager(PlayerStatsSO baseStat)
    {
        baseAttack = new PlayerStat(baseStat.baseAttackDamage);
        moveSpeed = new PlayerStat(baseStat.moveSpeed);
        baseattackSpeed = new PlayerStat(baseStat.baseattackSpeed);
        baseattackRange = new PlayerStat(baseStat.baseattackRange);
    }
}
