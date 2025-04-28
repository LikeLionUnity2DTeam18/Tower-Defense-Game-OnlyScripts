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
