using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element_Splash : TowerSplash
{
    [SerializeField]private Material mat;              // 할당된 머테리얼
    public float fadeDuration = 0.3f;   // 페이드 지속 시간
    public float delayA = 0.2f;       // A 시작 지연 시간

    private List<Collider2D> hitEnemies = new List<Collider2D>();

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") && !hitEnemies.Contains(collision))
        {
            hitEnemies.Add(collision);// 적이면 저장
        }
    }

    public void ClearHitEnemy()
    {
        hitEnemies.Clear();  //초기화
    }
    public void StartCharge()
    {
        StartCoroutine(Charge());
    }
    public void StartDischarge()
    {
        StartCoroutine(Discharge());
    }

    private IEnumerator Charge()
    {
        yield return StartCoroutine(ChangeInner(0.5f, 0f));
        yield return new WaitForSeconds(delayA);
        yield return StartCoroutine(ChangeOuter(0.5f, 0f));
    }

    private IEnumerator Discharge()
    {
        StartCoroutine(ChangeOuter(0f, 0.5f));
        yield return new WaitForSeconds(delayA);
        yield return StartCoroutine(ChangeInner(0f, 0.5f));

        // 디스차지 완료 후 데미지 적용
        foreach (Collider2D col in hitEnemies)
        {
            Collider2D enemy = col;
            if (enemy != null)
            {
                enemy.TryGetComponent<TowerStats>(out TowerStats targetStats);
                stats?.DoSpecialDamage(targetStats);
            }
        }
    }

    private IEnumerator ChangeOuter(float start, float end)
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            mat.SetFloat("_Outer", Mathf.Lerp(start, end, t / fadeDuration));
            t += Time.deltaTime;
            yield return null;
        }
        mat.SetFloat("_Outer", end);
    }
    private IEnumerator ChangeInner(float start, float end)
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            mat.SetFloat("_Inner", Mathf.Lerp(start, end, t / fadeDuration));
            t += Time.deltaTime;
            yield return null;
        }
        mat.SetFloat("_Inner", end);
    }

}
