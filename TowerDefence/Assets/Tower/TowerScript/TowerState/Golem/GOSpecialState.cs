using System.Collections;
using UnityEngine;

public class GOSpecialState : TSpecialState
{
    protected Golem golem => tower as Golem;
    public GOSpecialState(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        golem.StopAllCoroutines();
        golem.StartCoroutine(DelayReturnToIdle());
        golem.LeftFist.GetComponentInChildren<Animator>().SetBool("Smash", true);
        golem.RightFist.GetComponentInChildren<Animator>().SetBool("Smash", true);
    }
    public override void Update()
    {
    }
    public override void Exit()
    {
        base.Exit();
        golem.LeftFist.GetComponentInChildren<Animator>().SetBool("Smash", false);
        golem.RightFist.GetComponentInChildren<Animator>().SetBool("Smash", false);
    }

    IEnumerator DelayReturnToIdle()
    {
        yield return new WaitForSeconds(3f);
        towerFSM.ChangeState(tower.idleState);
    }
}
