using UnityEngine;

public class DeerMoveS : DeerSitS
{
    public DeerMoveS(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        if (tower.nearestEnemy == null) towerFSM.ChangeState(tower.fsmLibrary.deerAttackStandS);
        else if (tower.nearestMEnemy != null) towerFSM.ChangeState(tower.fsmLibrary.deerAttackSitS);
        if (triggerCalledStart)
        {
            tower.rb.linearVelocity = tower.dir * tower.moveSpeed;
        }
        else if(triggerCalledEnd)
        {
            tower.rb.linearVelocity = Vector2.zero;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

}
