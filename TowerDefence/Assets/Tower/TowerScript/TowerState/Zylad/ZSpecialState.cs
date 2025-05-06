using UnityEngine;

public class ZSpecialState : TSpecialState
{
    protected Zylad zylad => tower as Zylad;
    public ZSpecialState(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        if (triggerCalled2 && zylad.count == 0)
        {
            triggerCalled2 = false;
            zylad.Special1();
        }
        else if (triggerCalled2 && zylad.count == 1)
        {
            triggerCalled2 = false;
            zylad.Special2();
        }
        else if (triggerCalled2 && zylad.count == 2)
        {
            triggerCalled2 = false;
            zylad.Special3();
        }

        if (triggerCalled4)
        {
            triggerCalled4 = false;
            towerFSM.ChangeState(tower.idleState);
        }
    }
    public override void Exit()
    {
        base.Exit();
    }

}
