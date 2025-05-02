using UnityEngine;

public class DarkmurStats : TowerStats
{
    Darkmur darkmur;
    protected override void Awake()
    {
        base.Awake();
        darkmur = GetComponent<Darkmur>();
    }
    protected override void Die()
    {
        if (darkmur.isClone)
        {
            darkmur.CloneDestroy();
        }
        else
        {
            base.Die();
        }
    }
}
