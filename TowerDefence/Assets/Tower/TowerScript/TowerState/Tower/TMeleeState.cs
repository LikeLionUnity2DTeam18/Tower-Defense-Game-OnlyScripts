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

        if (triggerCalled5)
        {
            triggerCalled5 = false;
            tower.DoMeleeDamage();
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
