using UnityEngine;

public class AMeleeState : TMeleeState
{
    protected Azikel azikel => tower as Azikel;
    public AMeleeState(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        if (triggerCalled5)
        {
            triggerCalled5 = false;
            tower.DoMeleeDamage();
            azikel.CreateSlash();
        }
        if (tower.nearestMEnemy == null && triggerCalled1)
        {
            triggerCalled1 = false;
            towerFSM.ChangeState(tower.moveState);
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}
