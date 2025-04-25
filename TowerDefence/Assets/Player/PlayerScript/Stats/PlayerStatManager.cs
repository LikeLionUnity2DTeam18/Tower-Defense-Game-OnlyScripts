using UnityEngine;

public class PlayerStatManager
{
    public PlayerStat baseAttack;
    public PlayerStat moveSpeed;
    public PlayerStat attackSpeed;
    public PlayerStat attackRange;

    public PlayerStatManager(PlayerStatsSO baseStat)
    {
        baseAttack = new PlayerStat(baseStat.baseAttackDamage);
        moveSpeed = new PlayerStat(baseStat.moveSpeed);
        attackSpeed = new PlayerStat(baseStat.attackSpeed);
        attackRange = new PlayerStat(baseStat.attackRange);
    }
}
