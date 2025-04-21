using UnityEngine;

public class TowerState
{
    protected string stateName;
    protected Tower tower;
    protected TowerFSM towerFSM;


    protected bool triggerCalled = false;
    public TowerState(Tower tower,TowerFSM towerFSM, string stateName)
    {
        this.tower = tower;
        this.towerFSM = towerFSM;
        this.stateName = stateName;
    }

    public TowerState(Tower tower, TowerFSM towerFSM)
    {
        this.tower = tower;
        this.towerFSM = towerFSM;
    }


    public virtual void Enter()
    {
        if(stateName != null) tower.anim.SetBool(stateName, true);
        else return;
    }
    public virtual void Update()
    {
        if(triggerCalled)
        {
            towerFSM.ChangeState(tower.fsmLibrary.deerSitS);
        }
    }
    public virtual void Exit()
    {
        if (stateName != null) tower.anim.SetBool(stateName, false);
        else return;
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
