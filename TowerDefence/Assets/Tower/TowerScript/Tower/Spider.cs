using DG.Tweening;
using NUnit.Framework.Internal;
using System;
using System.Collections;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

public class Spider : Tower
{
    public GameObject proj;
    public Transform posRight;
    public Transform posLeft;
    [SerializeField] private float arcHeight = 2f;
    [SerializeField] private float duration = 1f;   // 한 바퀴 도는 시간
    public Tween spinTween; // 트윈 저장용
    public override void Awake()
    {
        base.Awake();

        fsmLibrary = new FSMLibrary(this, towerFSM);
        idleState = fsmLibrary.sIdleState;
        moveState = fsmLibrary.sMoveState;
        meleeState = fsmLibrary.sMeleeState;
        rangeState = fsmLibrary.sRangeState;
        specialState = fsmLibrary.sSpecialState;
    }
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    public void SpinWheel()
    {
        Vector3 start = transform.position;
        Vector3 end = nearestREnemy.transform.position;

        // 중간 점들 계산 (원호 형태 궤적)
        Vector3 mid1 = Vector3.Lerp(start, end, 0.5f) + Vector3.up * arcHeight;
        Vector3 mid2 = Vector3.Lerp(end, start, 0.5f) + Vector3.down * arcHeight;

        // 경로 설정
        Vector3[] path = new Vector3[] { start, mid1, end, mid2, start };

        //트윗 초기화
        spinTween?.Kill();

        // 경로 따라 이동
        spinTween = transform.DOPath(path, duration, PathType.CatmullRom)
                         .SetEase(Ease.InOutSine)
                         .OnComplete(() => {
                             fsmLibrary.sRangeState.AnimationTrigger1();
                             if (nearestREnemy != null)
                             {
                                anim.SetBool("Off", true);
                             }
                         });
    }

    public void Slash()
    {

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        GameObject slash = SpawnWithStats(proj);
        if(towerRight)
        {
            slash.transform.position = posRight.position;
        }
        else
        {
            slash.transform.position = posLeft.position;
        }
        slash.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}

