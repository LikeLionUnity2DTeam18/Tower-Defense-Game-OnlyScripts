using UnityEngine;

public class TowerStats : MonoBehaviour
{
    //참조
    DraggableTower Dtower; //드래그 타워 스크립트 참조
    [SerializeField] GameObject HitVFX;

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
    public void SetHP()
    {
        currentHealth = hp.GetValue();
    }

    void OnEnable()
    {
        isDead = false;
    }

    public virtual void DoMeleeDamage(EnemyController _targetStats)
    {
        //Debug.Log("DoMeleeDamage");
        float totalDamage = melee.GetValue();
        _targetStats.TakeDamage(totalDamage);
    }
    public virtual void DoRangeDamage(EnemyController _targetStats)
    {
        //Debug.Log("DoRangeDamage");
        float totalDamage = range.GetValue();
        _targetStats.TakeDamage(totalDamage);
    }
    public virtual void DoSpecialDamage(EnemyController _targetStats)
    {
        //Debug.Log("DoSpeicalDamage");
        float totalDamage = special.GetValue();
        _targetStats.TakeDamage(totalDamage);
    }

    private float lastDamageTime = -999f;
    public virtual void TakeDamage(float _damage)
    {
        if (isDead) return;
        if (Time.time - lastDamageTime < 1f) return; // 1초 쿨다운
        SoundManager.Instance.Play(SoundType.Hit, transform);
        lastDamageTime = Time.time;
        currentHealth -= _damage;
        if (HitVFX != null) 
        { 
            GameObject t = PoolManager.Instance.Get(HitVFX);
            t.transform.position = transform.position;
        }
        if (currentHealth < 0)
        {
            isDead = true;
            Die();
        }
    }
    private bool isDead = false;
    protected virtual void Die()
    {
        Dtower.ToIconWhenPlay();
    }
}
