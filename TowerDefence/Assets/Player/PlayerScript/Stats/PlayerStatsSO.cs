using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Player/PlayerStats")]
public class PlayerStatsSO : ScriptableObject
{
    public float baseAttackDamage;
    public float moveSpeed;
    public float attackSpeed; // 이름은 Speed지만 쿨다운처럼 작동 0.5면 초당2회 공격
    public float attackRange;

}
