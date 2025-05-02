using UnityEngine;

public static class EnemyStateFactory
{
    public static EnemyState CreateInitialState(EnemyController enemy, EnemyStateMachine stateMachine)
    {
        switch (enemy.Data.enemyType)
        {
            case EnemyType.Blocker:
                return new Blocker_IdleState(enemy, stateMachine);

            case EnemyType.Bloody:
                return new Bloody_IdleState(enemy, stateMachine);
            case EnemyType.Archer:
                return new Archer_IdleState(enemy, stateMachine);
            case EnemyType.Boomer:
                return new Boomer_IdleState(enemy, stateMachine);
            case EnemyType.Clawer:
                return new Clawer_IdleState(enemy, stateMachine);
            case EnemyType.Tanky:
                return new Tanky_IdleState(enemy, stateMachine);
            case EnemyType.BountyHunter:
                return new BountyHunter_IdleState(enemy, stateMachine);
            case EnemyType.Ghost:
                return new Ghost_IdleState(enemy, stateMachine);
            //여기에 case로 새로운 몬스터 추가

            default:
                UnityEngine.Debug.LogError($"[EnemyStateFactory] 등록되지 않은 EnemyType: {enemy.Data.enemyType}");
                return null;
        }
    }
}
