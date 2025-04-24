using UnityEngine;

public class PlayerArrow : PlayerHoamingProjectile
{
    public void Initialize(Transform _target, float _damage)
    {
        base.Initialize(_target);
        damage = _damage;
    }
}
