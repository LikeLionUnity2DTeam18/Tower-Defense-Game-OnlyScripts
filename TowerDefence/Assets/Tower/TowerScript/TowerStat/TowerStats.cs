using UnityEngine;

public class TowerStats : MonoBehaviour
{
    [Header("기본적인 스탯")]
    public TStat hp;
    public TStat melee;
    public TStat range;
    public TStat special;
    public TStat cooldown;
    public TStat speed;
    public TStat meleeDistance;
    public TStat moveDistance;
    public TStat rangeDistance;


    [SerializeField]
    private int currentHealth;



    protected virtual void Start()
    {
        currentHealth = hp.GetValue();
    }

    public virtual void DoMeleeDamage(TowerStats _targetStats)
    {
        int totalDamage = melee.GetValue();
        _targetStats.TakeDamage(totalDamage);
    }
    public virtual void DoRangeDamage(TowerStats _targetStats)
    {
        int totalDamage = range.GetValue();
        _targetStats.TakeDamage(totalDamage);
    }
    public virtual void DoSpecialDamage(TowerStats _targetStats)
    {
        int totalDamage = special.GetValue();
        _targetStats.TakeDamage(totalDamage);
    }

    public virtual void TakeDamage(int _damage)
    {
        currentHealth -= _damage;

        if (currentHealth < 0)
            Die();
    }

    protected virtual void Die()
    {

    }
}
