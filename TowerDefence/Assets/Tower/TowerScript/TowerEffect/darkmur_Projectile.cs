using UnityEngine;

public class darkmur_Projectile : TowerProjectile
{
    public override void Init(Vector2 dir)
    {
        direction = dir;
        startPos = transform.position;
        isReady = true;
    }
}
