using UnityEngine;

public class ZIdleState : TIdleState
{
    public ZIdleState(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        if (tower.nearestREnemy != null) towerFSM.ChangeState(tower.rangeState);
    }
    public override void Exit()
    {
        base.Exit();
    }
}
