using System.Collections;
using UnityEngine;

public class Zylad : Tower
{
    public int count = 0;
    public GameObject shotSword;
    public GameObject greatSword;
    public GameObject spear;
    private float currentAngle = 0f;
    public override void Awake()
    {
        base.Awake();

        fsmLibrary = new FSMLibrary(this, towerFSM);
        idleState = fsmLibrary.zIdleState;
        moveState = fsmLibrary.zMoveState;
        meleeState = fsmLibrary.zMeleeState;
        rangeState = fsmLibrary.zRangeState;
        specialState = fsmLibrary.zSpecialState;
    }
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        towerFSM.currentState.Update();

        //n초마다 특수스킬 발동
        if (timer > 0) timer -= Time.deltaTime;
        if (timer <= 0f && (nearestREnemy != null || nearestMEnemy != null || nearestEnemy != null))
        {
            timer = stats.cooldown.GetValue();
            towerFSM.ChangeState(specialState);
        }

        ChangeDir();
        if (GetoutArea() && nearestREnemy == null) transform.position = Beacon.transform.position;
    }

    public void Range1()
    {
        float baseAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float[] offsets = { -10f, 0f, 10f };

        foreach (float offset in offsets)
        {
            GameObject t = SpawnWithStats(shotSword);
            t.transform.position = transform.position;

            float angle = baseAngle + offset;
            t.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // 회전된 방향 벡터 계산해서 Init에 전달
            Vector3 rotatedDir = Quaternion.Euler(0, 0, offset) * dir;
            t.GetComponent<Zylad_Projectile>().Init(rotatedDir.normalized, transform.position);
        }
    }
    public void Range2()
    {
        float radius = 2f;

        // 각도에 따라 위치 계산
        Vector3 offset = new Vector3(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad)) * radius;

        GameObject t = SpawnWithStats(spear);
        t.transform.position = transform.position + offset;

        // 다음 호출을 위해 각도 증가
        currentAngle += 45f; // 예: 45도씩 증가
        if (currentAngle >= 360f) currentAngle -= 360f; // 0~360 유지
    }
    public void Range3()
    {
        GameObject t = SpawnWithStats(greatSword);
        t.transform.position = transform.position;
        t.GetComponent<Zylad_GreatSword>().Init(dir, transform);
    }

    public void Special1()
    {
        count++;
        anim.SetInteger("Speciall", count);
        StartCoroutine(RotatingSlash());
    }

    private IEnumerator RotatingSlash()
    {
        int totalShots = 72;
        float angleStep = 10f;
        float delay = 0.03f;

        float baseAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        for (int i = 0; i < totalShots; i++)
        {
            float angle = baseAngle + (i * angleStep);
            Vector3 rotatedDir = Quaternion.Euler(0, 0, angle) * Vector3.right;

            GameObject t = SpawnWithStats(shotSword);
            t.transform.position = transform.position;
            t.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            t.GetComponent<Zylad_Projectile>().Init(rotatedDir.normalized,transform.position);

            yield return new WaitForSeconds(delay);
        }
    }
    public void Special2()
    {
        count++;
        anim.SetInteger("Speciall", count);
        StartCoroutine(SpiralSpearBurst());
    }
    private IEnumerator SpiralSpearBurst()
    {
        float currentAngle = 0f;
        float radius = 1f;
        float angleStep = 30f;      // 각도 증가량 (작을수록 더 조밀하게 나선)
        float radiusStep = 0.1f;    // 반지름 증가량
        int spearCount = 30;
        float delay = 0.03f;        // 속도 (짧을수록 빠름)

        for (int i = 0; i < spearCount; i++)
        {
            Vector3 offset = new Vector3(
                Mathf.Cos(currentAngle * Mathf.Deg2Rad),
                Mathf.Sin(currentAngle * Mathf.Deg2Rad)
            ) * radius;

            GameObject t = SpawnWithStats(spear);
            t.transform.position = transform.position + offset;

            currentAngle += angleStep;
            radius += radiusStep;

            yield return new WaitForSeconds(delay);
        }
    }
    public void Special3()
    {
        count = 0;
        anim.SetInteger("Speciall", count);
        StartCoroutine(SpinningGreatSwordBurst());
    }
    private IEnumerator SpinningGreatSwordBurst()
    {
        int count = 12;                  // 총 발사 개수 (360도 → 30도 간격)
        float angleStep = 360f / count; // 각도 간격
        float delay = 0.05f;            // 발사 간격

        for (int i = 0; i < count; i++)
        {
            float angle = i * angleStep;
            Vector3 rotatedDir = Quaternion.Euler(0, 0, angle) * Vector3.right;

            GameObject t = SpawnWithStats(greatSword);
            t.transform.position = transform.position;
            t.GetComponent<Zylad_GreatSword>().Init(rotatedDir.normalized, transform);

            yield return new WaitForSeconds(delay);
        }
    }
}
