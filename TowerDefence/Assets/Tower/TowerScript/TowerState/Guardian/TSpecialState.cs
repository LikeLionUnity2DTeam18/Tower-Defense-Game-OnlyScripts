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
        if (triggerCalledEnd)
        {
            towerFSM.ChangeState(tower.fsmLibrary.idleState);
        }
    }
    public override void Exit()
    {
        base.Exit();
    }

}
