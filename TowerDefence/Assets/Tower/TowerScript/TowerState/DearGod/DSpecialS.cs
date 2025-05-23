using DG.Tweening;
using System.Buffers.Text;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class DSpecialS : TSpecialState
{
    DeerGod deerGod => tower as DeerGod;
    public DSpecialS(Tower tower, TowerFSM towerFSM, string stateName) : base(tower, towerFSM, stateName)
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
        if (triggerCalled2)
        {
            deerGod.transform.DOKill();
            deerGod.transform.DOMoveY(deerGod.transform.position.y + 2f, 0f).SetEase(Ease.OutQuad);
            triggerCalled2 = false;
        }
        if (triggerCalled3)
        {
            deerGod.transform.DOKill();
            deerGod.transform
                .DOMoveY(deerGod.transform.position.y - 2f, 0f)
                .SetEase(Ease.InQuad)
                .OnComplete(() => {
                    deerGod.FlowerSpawn();
                });
            triggerCalled3 = false;
        }
    }
}
