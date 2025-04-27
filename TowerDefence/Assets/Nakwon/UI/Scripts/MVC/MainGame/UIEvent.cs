using UnityEngine;

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
