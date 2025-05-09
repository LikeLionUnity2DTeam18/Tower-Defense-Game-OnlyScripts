using UnityEngine;

public class GameWaveState : GameState
{

    private StageData stageData; 
    public GameWaveState(GameStateMachine stateMachine, PlayerInputHandler input, GameManager game) : base(stateMachine, input, game)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stageData = game.StageDataList[game.CurrentStage - 1];
        stateTimer = stageData.StageDuration;
        EventManager.Trigger(new StageChangeEvent(StageChangeEventType.Start, game.CurrentStage,stageData));

        Debug.Log($"스태이지 시작, {game.CurrentStage} 스태이지");
        // 해당 스태이지 스폰 시작
    }

    public override void Exit()
    {
        base.Exit();

        // 스태이지 보상 획득
        Debug.Log($"스태이지 보상 골드 획득 : {stageData.GoldRewardOnEnd} 골드");
        EventManager.Trigger(new MonsterDied(stageData.GoldRewardOnEnd, PlayerManager.Instance.Player.transform.position));

        game.ProceedStage();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer <= 0)
            stateMachine.ChangeState(game.BuildState);
    }
}
