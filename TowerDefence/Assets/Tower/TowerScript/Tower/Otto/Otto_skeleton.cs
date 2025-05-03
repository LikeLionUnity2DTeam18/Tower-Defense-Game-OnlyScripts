using System;
using UnityEngine;

public class Otto_skeleton : MonoBehaviour
{
    public bool towerFront { get; private set; } = true;//앞인지 뒤인지
    public bool towerRight { get; private set; } = true;//오른쪽인지 왼쪽인지

    public Vector2 dir;
    protected SpriteRenderer towerSprite;     //플립용
    public Animator anim { get; protected set; }
    public Rigidbody2D rb { get; protected set; }
    public GameObject nearestMEnemy { get; private set; } //근접 적
    public GameObject nearestEnemy { get; private set; }
    public TowerStats stats { get; protected set; }
    public Tower tower { get; protected set; }
    public bool trigger { get; private set; } = false;
    public bool trigger1 { get; private set; } = false;
    public GameObject prefab;


    public void Awake()
    {
        towerSprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<TowerStats>();
    }

    public void Update()
    {
        ChangeDir();
        State();
        if (trigger)
        {
            trigger = false;
            DoMeleeDamage();
        }
        else if (trigger1)
        {
            trigger1 = false;
            GameObject a = SpawnWithStats(prefab);
            a.transform.position = transform.position;
            PoolManager.Instance.Return(gameObject);
        }
    }

    //자폭
    public void OnSpecial()
    {
        anim.SetBool("Special", true);
        anim.SetBool("Attack", false);
        anim.SetBool("Move", false);
    }

    void OnEnable() => Otto.OnAnyOttoSpecial += OnSpecial;
    void OnDisable() => Otto.OnAnyOttoSpecial -= OnSpecial;

    public void State()
    {
        if(nearestMEnemy != null)
        {
            anim.SetBool("Attack", true);
            anim.SetBool("Move", false);
        }
        else if(nearestEnemy != null)
        {
            anim.SetBool("Attack", false);
            anim.SetBool("Move", true);
            rb.MovePosition(rb.position + dir * stats.speed.GetValue() * Time.fixedDeltaTime);
        }
    }

    public void Trigger()
    {
        trigger = true;
    }
    public void Trigger1()
    {
        trigger1 = true;
    }

    //근접 데미지 함수
    public void DoMeleeDamage()
    {
        DamageAllEnemiesInRange(transform.position, stats.meleeDistance.GetValue(), LayerMask.GetMask("Enemy"), stats.DoMeleeDamage);
    }
    public void DoSpecialDamage()
    {
        DamageAllEnemiesInRange(transform.position, stats.meleeDistance.GetValue(), LayerMask.GetMask("Enemy"), stats.DoSpecialDamage);
    }
    private void DamageAllEnemiesInRange(Vector3 origin, float radius, LayerMask enemyLayer, Action<TowerStats> damageFunc)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(origin, radius, enemyLayer);

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent<TowerStats>(out var target))
            {
                damageFunc(target);
            }
        }
    }
    public GameObject SpawnWithStats(GameObject prefab)
    {
        GameObject obj = PoolManager.Instance.Get(prefab);

        if (obj.TryGetComponent<IStatReceiver>(out var receiver))
        {
            receiver.SetStats(tower, this.stats);
        }
        return obj;
    }

    #region 방향설정
    protected void ChangeDir()
    {
        nearestMEnemy = FindNearestEnemyByOverlap(transform.position, stats.meleeDistance.GetValue(), LayerMask.GetMask("Enemy"));
        if (nearestMEnemy != null)
        {
            UpdateDirection(nearestMEnemy.transform.position);
            dir = (nearestMEnemy.transform.position - transform.position).normalized;
        }
        else if (nearestMEnemy == null) 
        {
            nearestEnemy = FindNearestEnemyByOverlap(transform.position, stats.moveDistance.GetValue(), LayerMask.GetMask("Enemy"));
            if (nearestEnemy != null)
            {
                UpdateDirection(nearestEnemy.transform.position);
                dir = (nearestEnemy.transform.position - transform.position).normalized;
            }
        }

            //좌, 우 변경
            Flip();
        //앞, 뒤 변경
        UpDown();
    }


    public GameObject FindNearestEnemyByOverlap(Vector3 origin, float radius, LayerMask enemyLayer)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(origin, radius, enemyLayer);
        if (hits.Length == 0) return null;

        GameObject nearest = null;
        float minDist = float.MaxValue;

        foreach (var hit in hits)
        {
            float dist = (hit.transform.position - origin).sqrMagnitude;

            if (dist < minDist)
            {
                minDist = dist;
                nearest = hit.gameObject;
            }
        }
        return nearest;
    }

    void UpdateDirection(Vector2 enemyPos)
    {
        if (enemyPos == null)
        {
            return;
        }
        else if (enemyPos != null)
        {
            var quadrant = TowerDirection.GetQuadrant(transform.position, enemyPos); switch (quadrant)
            {
                case Quadrant.Q1:
                    towerFront = false;
                    towerRight = true;
                    break;
                case Quadrant.Q2:
                    towerFront = false;
                    towerRight = false;
                    break;
                case Quadrant.Q3:
                    towerFront = true;
                    towerRight = false;
                    break;
                case Quadrant.Q4:
                    towerFront = true;
                    towerRight = true;
                    break;
            }
        }
    }

    void Flip()
    {
        if (towerRight)
        {
            towerSprite.flipX = false;
        }
        else if (!towerRight)
        {
            towerSprite.flipX = true;
        }
    }
    public void UpDown()
    {
        //앞 레이어만 있을 경우 레이어 변경 안함
        if (anim.layerCount <= 1) return;

        //앞 뒤 변경
        if (!anim.IsInTransition(1) && !anim.IsInTransition(2))
        {
            if (towerFront)
            {
                anim.SetLayerWeight(1, 1f);
                anim.SetLayerWeight(2, 0f);
            }
            else
            {
                anim.SetLayerWeight(1, 0f);
                anim.SetLayerWeight(2, 1f);
            }
        }
    }
    #endregion
}
