using UnityEngine;

public class DMoveS : TowerState
{
    public DMoveS(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (tower.nearestMEnemy != null) towerFSM.ChangeState(tower.fsmLibrary.dIdleS);
        else if (tower.nearestEnemy == null) towerFSM.ChangeState(tower.fsmLibrary.dIdleS);
        tower.rb.linearVelocity = tower.dir * tower.moveSpeed;
    }
}
