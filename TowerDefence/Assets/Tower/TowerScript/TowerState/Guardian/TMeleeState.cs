using UnityEngine;

public class TMeleeState : TowerState
{
    public TMeleeState(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        base.Update();
        if (tower.nearestMEnemy == null) towerFSM.ChangeState(tower.fsmLibrary.idleState);
    }
    public override void Exit()
    {
        base.Exit();
    }
}
