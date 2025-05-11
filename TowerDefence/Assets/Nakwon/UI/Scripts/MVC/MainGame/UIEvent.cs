using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// 골드가 변경됐을 때
/// </summary>
public struct GoldChanged
{
    public int NewGold { get; private set; }
    public GoldChanged(int gold) => NewGold = gold;
}

public struct GoldSpended
{
    public int Amount {get; private set;}
    public GoldSpended(int amount)=> Amount = amount;
}

//몬스터가 죽으면 보상 골드와 죽은 위치를 전달
public struct MonsterDied
{
    public int RewardGold { get; private set; }
    public Vector3 Position { get; private set; }
    public MonsterDied(int rewardGold, Vector3 position)
    {
        RewardGold = rewardGold;
        Position = position;
    }
}

// 기지 체력이 닳았을 때
public struct BaseTowerHealthChanged
{
    public int CurrentHealth {get; private set;}
    public int MaxHealth {get; private set;}

    public BaseTowerHealthChanged(int _CurrentHealth, int _MaxHealth)
    {
        CurrentHealth = _CurrentHealth;
        MaxHealth = _MaxHealth;
    }
}

//스피드 버튼 눌렸을 때
public struct SpeedChanged
{
    public SpeedType NewSpeed;

    public SpeedChanged(SpeedType speed)
    {
        NewSpeed = speed;
    }
}

public struct EnemySpawned
{
    public Transform enemyTransform;

    public EnemySpawned(Transform enemyTransform)
    {
        this.enemyTransform = enemyTransform;
    }
}

public struct TowerSpawned
{
    public Transform towerTransform;

    public TowerSpawned(Transform towerTransform)
    {
        this.towerTransform = towerTransform;
    }
}

/// <summary>
/// 툴팁이 표시되어야 할 때
/// </summary>
public struct TooltipRequested
{
    public string Message { get; private set; }
    public Vector2 Position { get; private set; }

    public TooltipRequested(string message, Vector2 position)
    {
        Message = message;
        Position = position;
    }
}

/// <summary>
/// 일시정지/해제
/// </summary>
public struct GamePaused
{
    public bool IsPaused;
    public GamePaused(bool ispaused)
    {
        IsPaused = ispaused;
    }
}

/// <summary>
/// 시작버튼 클릭(or 스페이스바)
/// </summary>
public struct StartButtonClick
{

}

/// <summary>
/// 스태이지 시작 이벤트
/// </summary>
public struct GameStarted
{

}

/// <summary>
/// 인벤토리 온오프
/// </summary>
public struct ToggleInventory
{
}

/// <summary>
/// 인게임 메뉴 온오프
/// </summary>
public struct ToggleMenu
{

}

/// <summary>
/// 인게임 메뉴 버튼 3개
/// </summary>
public enum MenuButtonTypes { Start, Option, Exit}
public struct MenuButtonClicked
{
    public MenuButtonTypes type;

    public MenuButtonClicked(MenuButtonTypes type) 
        {
            this.type = type;
        }
}

public struct StageNumberChanged
{
    public int NewStage { get; private set; }
    public StageNumberChanged(int stage) => NewStage = stage;
}