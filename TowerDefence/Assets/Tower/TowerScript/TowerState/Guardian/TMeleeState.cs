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
        if (tower.nearestMEnemy == null && triggerCalledEnd)
        { 
            towerFSM.ChangeState(tower.fsmLibrary.moveState); 
            triggerCalledEnd = false;
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}
