using System.Net.NetworkInformation;
using System.Threading;
using DG.Tweening;
using UnityEngine;

public class Guardian_Projectile : TowerGuidance
{
    private float duration = 10f;
    [SerializeField]private float timer = 10f;
    private bool isShrinking;

    protected override void Update()
    {
        base.Update();
        Timer();
        DurationEnd();
    }

    private void Timer()
    {
        if(timer > 0)timer -= Time.deltaTime;
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
        transform.localScale = Vector3.one;
        transform.DOKill();
    }

    public void Shirink()
    {
        float shrinkTime = 1f; // 축소에 걸리는 시간
        transform.DOScale(Vector3.zero, shrinkTime)
            .SetEase(Ease.InBack)
            .OnComplete(() =>
            {
                PoolManager.Instance.Return(gameObject);
                ResetProjectile();
            });
    }
}