using UnityEngine;

public class DeerAttackStandS : DeerStandS
{
    public DeerAttackStandS(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        if (tower.nearestMEnemy != null)
        {
            towerFSM.ChangeState(tower.fsmLibrary.deerIdleSitS);
        }
        else if (tower.nearestEnemy != null)
        {
            towerFSM.ChangeState(tower.fsmLibrary.deerIdleSitS);
        }
        else if (tower.nearestREnemy == null)
        {
            towerFSM.ChangeState(tower.fsmLibrary.deerIdleSitS);
        }
        if (triggerCalledStart)
        {
            deerGod.StartSpikeAttack(tower.transform.position, tower.nearestREnemy.transform.position);
            triggerCalledStart = false;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

}
