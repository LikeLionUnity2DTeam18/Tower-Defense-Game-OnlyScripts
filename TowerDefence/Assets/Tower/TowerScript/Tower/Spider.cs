using DG.Tweening;
using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Spider : Tower
{
    public GameObject proj;
    public Transform pos;

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
}
