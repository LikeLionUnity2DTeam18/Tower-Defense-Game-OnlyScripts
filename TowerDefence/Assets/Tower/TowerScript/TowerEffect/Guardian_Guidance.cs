using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static UnityEngine.GraphicsBuffer;

public class Guardian_Guidance : TowerGuidance
{
    private float duration = 10f;
    [SerializeField] private float timer = 10f;
    private bool isShrinking;

    private float tickRate = 0.5f; // 데미지 주기 간격 (초)
    private float tickTimer = 0f;

    public override void Update()
    {
        base.Update();
        Timer();
        DurationEnd();
        DoTickDamage();
    }

    private void Timer()
    {
        if (timer > 0) timer -= Time.deltaTime;
    }

    private void DurationEnd()
    {
        if (timer <= 0 && !isShrinking)
        {
            isShrinking = true;
            Shirink();
        }
    }

    public void ResetProjectile()
    {
        timer = duration;
        isShrinking = false;
        tickTimer = 0f;
        transform.DOKill(transform);
        transform.localScale = Vector3.one;
    }

    public void Shirink()
    {
        float shrinkTime = 1f;
        transform.DOScale(Vector3.zero, shrinkTime)
            .SetUpdate(true)  // 렌더링 독립적 실행 (렉 방지)
            .SetAutoKill(true)
            .SetLink(gameObject, LinkBehaviour.KillOnDisable)
            .OnComplete(() =>
            {
                PoolManager.Instance.Return(gameObject);
                ResetProjectile();
            });
    }

    private void DoTickDamage()
    {
        tickTimer += Time.deltaTime;
        if (tickTimer < tickRate) return;

        if (target != null && target.TryGetComponent<TowerStats>(out var statsTarget))
        {
            stats?.DoSpecialDamage(statsTarget);
        }

        tickTimer = 0f;
    }
}
