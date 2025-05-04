using DG.Tweening;
using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WatchDog : Tower
{
    public GameObject wave;
    public Transform pos;

    public override void Awake()
    {
        base.Awake();

        fsmLibrary = new FSMLibrary(this, towerFSM);
        idleState = fsmLibrary.wIdleState;
        moveState = fsmLibrary.wMoveState;
        meleeState = fsmLibrary.wMeleeState;
        rangeState = fsmLibrary.wRangeState;
        specialState = fsmLibrary.wSpecialState;
    }
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    public void ShieldBash()
    {
        GameObject target = nearestREnemy;
        if (target != null)
        {
            Vector2 targetPos = target.transform.position;

            transform.DOMove(targetPos, 0.2f).SetEase(Ease.OutQuad);
        }
    }

    public void CreateWave(Vector2 startPos, Vector2 targetPos)
    {
        if(targetPos == null) return;
        Vector2 dir = (targetPos - startPos).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        GameObject test = SpawnWithStats(wave);
        test.transform.position = pos.position;
        test.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        test.GetComponent<WatchDog_Stay>().RendReset();
    }
}
