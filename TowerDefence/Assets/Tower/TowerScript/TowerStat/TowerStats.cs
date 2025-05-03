using UnityEngine;

public class TowerStats : MonoBehaviour
{
    //참조
    DraggableTower Dtower; //드래그 타워 스크립트 참조

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
    private float currentHealth;

    protected virtual void Awake()
    {
        Dtower = GetComponent<DraggableTower>();
    }

    protected virtual void Start()
    {
        currentHealth = hp.GetValue();
    }

    public virtual void DoMeleeDamage(TowerStats _targetStats)
    {
        if (_targetStats == null)
        {
            Debug.LogError("타겟 스탯 null");
            return;
        }

        float totalDamage = melee.GetValue();
        _targetStats.TakeDamage(totalDamage);
    }
    public virtual void DoRangeDamage(TowerStats _targetStats)
    {
        float totalDamage = range.GetValue();
        _targetStats.TakeDamage(totalDamage);
    }
    public virtual void DoSpecialDamage(TowerStats _targetStats)
    {
        float totalDamage = special.GetValue();
        _targetStats.TakeDamage(totalDamage);
    }

    public virtual void TakeDamage(float _damage)
    {
        currentHealth -= _damage;
        Debug.Log("데미지"+_damage);
        if (currentHealth < 0)
            Die();
    }

    protected virtual void Die()
    {
        Dtower.ToIcon();
    }
}
