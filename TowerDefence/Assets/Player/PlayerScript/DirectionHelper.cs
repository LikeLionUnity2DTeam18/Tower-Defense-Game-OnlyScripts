using UnityEngine;

/// <summary>
/// 벡터에서 방향, 방향에서 벡터를 편하게 변환하는데 쓰는 클래스
/// </summary>
public enum Direction4Custom
{
    NW = 0,
    NE = 1,
    SW = 2,
    SE = 3
}

public static class DirectionHelper
{
    /// <summary>
    /// 벡터을 NW,NE,SW,SE 4방향으로 변환
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    public static Direction4Custom ToDirection4Custom(Vector2 dir)
    {
        if (dir.x >= 0 && dir.y > 0) return Direction4Custom.NE;
        else if (dir.x >= 0 && dir.y <= 0) return Direction4Custom.SE;
        else if (dir.x < 0 && dir.y > 0) return Direction4Custom.NW;
        else return Direction4Custom.SW;
    }

    /// <summary>
    /// 방향을 입력하면 그 방향에 맞는 단위벡터를 반환
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    public static Vector2 ToDirectionVector(Direction4Custom dir)
    {
        switch (dir)
        {
            case Direction4Custom.NW:
                return new Vector2(-1, 1).normalized;
            case Direction4Custom.NE:
                return new Vector2(1, 1).normalized;
            case Direction4Custom.SW:
                return new Vector2(-1, -1).normalized;
            case Direction4Custom.SE:
                return new Vector2(1, -1).normalized;
            default:
                return Vector2.zero;
        }
    }

    /// <summary>
    /// 방향에 맞게 애니메이션 파라미터 셋팅
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    public static Vector2 ToAnimParamVector(Direction4Custom dir)
    {
        switch (dir)
        {
            case Direction4Custom.NW:
                return new Vector2(-1, 1);
            case Direction4Custom.NE:
                return new Vector2(1, 1);
            case Direction4Custom.SW:
                return new Vector2(-1, -1);
            case Direction4Custom.SE:
                return new Vector2(1, -1);
            default:
                return Vector2.zero;
        }
    }

    public static int ToAnimParam(Direction4Custom dir)
    {
        return (int)dir;
    }
}
