using UnityEngine;
using UnityEngine.UI;

public class MinimapMarkerFollow : MonoBehaviour
{
    private Transform target;
    private RectTransform markerRect;
    private RectTransform minimapArea;
    private ObjectPool pool;
    private float mapSizeWorld;

    private bool isInitialized = false;

    public void Init(Transform target, Color color, ObjectPool pool, RectTransform minimapArea, float mapSizeWorld)
    {
        this.target = target;
        this.pool = pool;
        this.minimapArea = minimapArea;
        this.mapSizeWorld = mapSizeWorld;

        markerRect = GetComponent<RectTransform>();

        color.a = 1f; // 알파값 1로 초기화
        GetComponent<Image>().color = color;

        isInitialized = true;
        enabled = true; //재사용 시 다시 켜줘야 함
    }

    private void Update()
    {
        if (!isInitialized || pool == null || minimapArea == null || markerRect == null)
            return;

        // target이 null이거나 비활성화되었거나, 파괴 직전인 경우
        if (!target || !target.gameObject.activeSelf)
        {
            pool.Return(gameObject);
            target = null; //이전 타겟 완전 제거
            isInitialized = false;
            enabled = false;
            return;
        }
        float halfMap = mapSizeWorld * 0.5f;
        float normalizedX = (target.position.x + halfMap) / mapSizeWorld;
        float normalizedY = (target.position.y + halfMap) / mapSizeWorld;

        float uiX = (normalizedX - 0.5f) * minimapArea.rect.width;
        float uiY = (normalizedY - 0.5f) * minimapArea.rect.height;

        markerRect.anchoredPosition = new Vector2(uiX, uiY);
    }

}
