using UnityEngine;

public class WSpecialState : TSpecialState
{
    protected WatchDog watchDog => tower as WatchDog;
    public WSpecialState(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        base.Update();
        if (triggerCalledStart)
        {
            watchDog.CreateWave(watchDog.pos.transform.position,tower.nearestREnemy.transform.position);
            triggerCalledStart = false;
        }
    }
    public override void Exit()
    {
        base.Exit();
    }

}
