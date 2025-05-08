using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 타워 강화
/// </summary>
public class PowerUpController : PlayerSkillEntity
{
    private CapsuleCollider2D cd;
    private int damageUpAmount;  // 공격력 버프 수치
    private float checkColiiderPeriod = 0.5f;  // 버프 대상 탐색 주기
    private float checkColliderTimer;
    private float Aoe;

    private List<TowerStats> buffList;  // 버프 받은 대상 리스트
    private void Start()
    {
        cd = GetComponentInChildren<CapsuleCollider2D>();
        buffList = new();
    }
    /// <summary>
    /// 지속시간 종료시 버프 제거 
    /// </summary>
    protected override void OnDurationEnd()
    {
        RemoveAllBuffsOnDurationEnd();
        Release();
    }


    /// <summary>
    /// 버프대상탐색주기마다 탐색
    /// </summary>
    protected override void Update()
    {
        base.Update();

        if (!isActive) return;
        checkColliderTimer -= Time.deltaTime;
        if (checkColliderTimer < 0)
        {
            checkColliderTimer = checkColiiderPeriod;
            CheckColliderAndAddToList();
        }

    }

    /// <summary>
    /// 스킬 생성 시 초기화
    /// </summary>
    /// <param name="position"></param>
    /// <param name="damageUpAmount"></param>
    /// <param name="duration"></param>
    public void SetPowerUp(Vector2 position, int damageUpAmount, float duration, float AoE)
    {
        transform.position = position;
        durationTimer = duration;
        this.damageUpAmount = damageUpAmount;
        SetEntity(true);

        transform.localScale *= AoE;
    }

    /// <summary>
    /// 콜라이더랑 겹치는 대상 탐색 후 타워만 리스트에 넣고 버프 적용
    /// </summary>
    private void CheckColliderAndAddToList()
    {
        ContactFilter2D contactFilter = new ContactFilter2D().NoFilter();
        List<Collider2D> targets = new();

        int count = Physics2D.OverlapCollider(cd, contactFilter, targets);

        foreach (var target in targets)
        {
            TowerStats col = target.gameObject.GetComponent<TowerStats>();
            if (col != null && !buffList.Contains(col))
            {
                AddToListAndBuff(col);
            }
        }
    }

    /// <summary>
    /// 근접,원거리,특수공격 데미지 버프 적용
    /// </summary>
    /// <param name="col"></param>
    private void AddToListAndBuff(TowerStats col)
    {
        buffList.Add(col);
        col.melee.AddModifier(damageUpAmount);
        col.range.AddModifier(damageUpAmount);
        col.special.AddModifier(damageUpAmount);
    }

    /// <summary>
    /// 지속시간 끝나면 줬던 버프 제거
    /// 중간에 타워 사라졌는지 널체크 추가 
    /// </summary>
    private void RemoveAllBuffsOnDurationEnd()
    {
        foreach (var col in buffList)
        {
            col?.melee.RemoveModifier(damageUpAmount);
            col?.range.RemoveModifier(damageUpAmount);
            col?.special.RemoveModifier(damageUpAmount);
        }
        buffList.Clear();
    }
}
