using UnityEngine;

public class TowerState
{
    protected string stateName;
    protected Tower tower;
    protected TowerFSM towerFSM;


    protected bool triggerCalled1; //end
    protected bool triggerCalled2; //start
    protected bool triggerCalled3; //called
    protected bool triggerCalled4; //special
    protected bool triggerCalled5;
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
        tower.UpDown();
        if(stateName != null) tower.anim.SetBool(stateName, true);
        else return;
        TriggerSet();
    }
    public virtual void Update()
    {
        tower.TowerStop();
    }
    public virtual void Exit()
    {
        if (stateName != null) tower.anim.SetBool(stateName, false);
        else return;
        TriggerSet();
    }

    public virtual void AnimationTrigger1()
    {
        triggerCalled1 = true;
    }
    public virtual void AnimationTrigger2()
    {
        triggerCalled2 = true;
    }
    public virtual void AnimationTrigger3()
    {
        triggerCalled3 = true;
    }
    public virtual void AnimationTrigger4()
    {
        triggerCalled4 = true;
    }
    public virtual void AnimationTrigger5()
    {
        triggerCalled5 = true;
    }   

    private void TriggerSet()
    {
        triggerCalled1 = false;
        triggerCalled2 = false;
        triggerCalled3 = false;
        triggerCalled4 = false;
        triggerCalled5 = false;
    }
}
