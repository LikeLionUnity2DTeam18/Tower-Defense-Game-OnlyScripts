using UnityEngine;

public class GameWaveState : GameState
{

    private StageData stageData;
    private bool isSpawnEnd;

    private float findEnemyTimer;
    private float findEnemyInterval = 3f;

    public GameWaveState(GameStateMachine stateMachine, PlayerInputHandler input, GameManager game) : base(stateMachine, input, game)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stageData = game.StageDataList[game.CurrentStage - 1];
        stateTimer = stageData.StageDuration;
        EventManager.Trigger(new StageChangeEvent(StageChangeEventType.Start, game.CurrentStage,stageData));

        isSpawnEnd = false;
        Debug.Log($"스태이지 시작, {game.CurrentStage} 스태이지");
        // 해당 스태이지 스폰 시작
    }

    public override void Exit()
    {
        base.Exit();

        // 스태이지 보상 획득
        Debug.Log($"스태이지 보상 골드 획득 : {stageData.GoldRewardOnEnd} 골드");
        EventManager.Trigger(new MonsterDied(stageData.GoldRewardOnEnd, PlayerManager.Instance.Player.transform.position));
        EventManager.Trigger(new StageChangeEvent(StageChangeEventType.End, game.CurrentStage,stageData));
        for (int i = 0; i < stageData.NumberOfItems; i++)
        {
            InventoryManager.Instance.AddRandomPossibleItemToInventory();
        }

        game.ProceedStage();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer <= 0)
        {
            isSpawnEnd = true;

            EventManager.Trigger(new StageChangeEvent(StageChangeEventType.SpawnEnd, game.CurrentStage,stageData));
        }

        if(isSpawnEnd)
        {
            //살아있는 Enemy 체크, 없으면 리턴
            findEnemyTimer -= Time.deltaTime;
            if(findEnemyTimer < 0)
            {
                findEnemyTimer = findEnemyInterval;
                //적찾기

                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                if(enemies.Length == 0)
                    stateMachine.ChangeState(game.BuildState);
            }
        }

            
    }
}
