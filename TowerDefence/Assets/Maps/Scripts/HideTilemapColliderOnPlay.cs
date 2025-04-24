using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// 콜라이더 타일맵이 호출될 때, 렌더러를 없애는 클래스.
/// </summary>
public class HideTilemapColliderOnPlay : MonoBehaviour
{
    /// <summary>
    /// 타일맵 렌더러
    /// </summary>
    private TilemapRenderer tilemapRenderer;

    /// <summary>
    /// 호출 될 때 타일맵 렌더러를 끄고 콜라이더만 남긴다.
    /// </summary>
    void Start()
    {
        tilemapRenderer = GetComponent<TilemapRenderer>(); //타일맵 렌더러 가져오기
        tilemapRenderer.enabled = false; //타일맵 렌더러 끄기
    }

}
