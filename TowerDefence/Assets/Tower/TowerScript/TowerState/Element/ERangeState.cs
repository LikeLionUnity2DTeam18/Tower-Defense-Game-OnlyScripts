using UnityEngine;

public class ERangeState : TRangeState
{
    protected Element element => tower as Element;
    public ERangeState(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        towerFSM.ChangeState(tower.idleState);
    }
    public override void Exit()
    {
        base.Exit();
    }
}
public class EFRangeState : TRangeState
{
    protected ElementFire element => tower as ElementFire;
    public EFRangeState(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        base.Update();

        if (triggerCalled3 && tower.nearestREnemy != null)
        {
            triggerCalled3 = false;
            element.FireBreath();
        }
    }
    public override void Exit()
    {
        base.Exit();
        element.FireBreathEnd();
    }
}
public class EWRangeState : TRangeState
{
    protected ElementWater element => tower as ElementWater;
    public EWRangeState(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        base.Update();

        if (triggerCalled3 && tower.nearestREnemy != null)
        {
            triggerCalled3 = false;
            element.Tsunami();
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}
