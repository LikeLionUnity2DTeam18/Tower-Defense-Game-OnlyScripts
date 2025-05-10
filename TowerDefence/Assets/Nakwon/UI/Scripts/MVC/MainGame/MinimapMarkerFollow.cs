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
    private Image markerImage;

    public void Init(Transform target, Color color, ObjectPool pool, RectTransform minimapArea, float mapSizeWorld)
    {
        this.target = target;
        this.pool = pool;
        this.minimapArea = minimapArea;
        this.mapSizeWorld = mapSizeWorld;

        markerRect = GetComponent<RectTransform>();
        markerImage = GetComponent<Image>();

        color.a = 1f; // 알파값 1로 초기화
        markerImage.color = color;

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

        //범위 안에 있는지 판별
        bool isInside = normalizedX >= 0f && normalizedX <= 1f &&
                        normalizedY >= 0f && normalizedY <= 1f;

        markerImage.enabled = isInside; // 보여주기 / 숨기기
        
        float uiX = (normalizedX - 0.5f) * minimapArea.rect.width;
        float uiY = (normalizedY - 0.5f) * minimapArea.rect.height;

        markerRect.anchoredPosition = new Vector2(uiX, uiY);
    }

}
