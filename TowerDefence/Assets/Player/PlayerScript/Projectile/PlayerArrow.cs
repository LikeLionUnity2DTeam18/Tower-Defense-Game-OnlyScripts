using UnityEngine;

/// <summary>
/// 플레이어 기본 공격
/// </summary>
public class PlayerArrow : PlayerHoamingProjectile
{
    public void Initialize(Vector2 _position, EnemyController _target, float _damage)
    {
        base.Initialize(_position, _target);
        damage = _damage;
    }


}
