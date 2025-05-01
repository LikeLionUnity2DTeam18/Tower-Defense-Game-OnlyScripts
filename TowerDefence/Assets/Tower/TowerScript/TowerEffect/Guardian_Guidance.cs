using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Guardian_Guidance : TowerGuidance
{
    private float duration = 10f;
    [SerializeField] private float timer = 10f;
    private bool isShrinking;

    private float tickRate = 0.1f; // 데미지 주기 간격 (초)
    private float tickTimer = 0f;

    private HashSet<TowerStats> enemiesInRange = new HashSet<TowerStats>();

    protected override void Update()
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
        transform.localScale = Vector3.one;
        transform.DOKill();
        enemiesInRange.Clear();
    }

    public void Shirink()
    {
        float shrinkTime = 1f;
        transform.DOScale(Vector3.zero, shrinkTime)
            .SetEase(Ease.InBack)
            .OnComplete(() =>
            {
                PoolManager.Instance.Return(gameObject);
                ResetProjectile();
            });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Enemy")) return;

        if (collision.TryGetComponent<TowerStats>(out TowerStats targetStats))
        {
            enemiesInRange.Add(targetStats);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<TowerStats>(out TowerStats targetStats))
        {
            enemiesInRange.Remove(targetStats);
        }
    }

    private void DoTickDamage()
    {
        tickTimer += Time.deltaTime;
        if (tickTimer >= tickRate)
        {
            foreach (var enemy in enemiesInRange)
            {
                if (enemy != null)
                {
                    stats?.DoSpecialDamage(enemy);
                }
            }

            tickTimer = 0f;
        }
    }
}
