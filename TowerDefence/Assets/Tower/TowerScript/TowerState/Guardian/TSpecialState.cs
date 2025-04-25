using UnityEngine;

public class TSpecialState : TowerState
{
    public TSpecialState(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        base.Update();
        if (triggerCalled)
        {
            towerFSM.ChangeState(tower.fsmLibrary.idleState);
            triggerCalled = false;
        }
    }
    public override void Exit()
    {
        base.Exit();
    }

}
