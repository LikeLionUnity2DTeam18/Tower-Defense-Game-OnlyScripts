using UnityEditor;
using UnityEngine;

public class TMoveState : TowerState
{
    public TMoveState(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        base.Update();
        if (tower.nearestMEnemy != null) towerFSM.ChangeState(tower.fsmLibrary.meleeState);
        else if (tower.nearestEnemy == null) towerFSM.ChangeState(tower.fsmLibrary.rangeState);
        tower.rb.linearVelocity = tower.dir * tower.moveSpeed;
    }
    public override void Exit()
    {
        base.Exit();
    }
}
