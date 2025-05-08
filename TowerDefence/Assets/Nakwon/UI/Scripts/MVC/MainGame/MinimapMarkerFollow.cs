using UnityEngine;
using UnityEngine.UI;

public class MinimapMarkerFollow : MonoBehaviour
{
    private Transform target;
    private RectTransform markerRect;
    private RectTransform minimapArea;
    private ObjectPool pool;
    private float mapSizeWorld;

    public void Init(Transform target, Color color, ObjectPool pool, RectTransform minimapArea, float mapSizeWorld)
    {
        this.target = target;
        this.pool = pool;
        this.minimapArea = minimapArea;
        this.mapSizeWorld = mapSizeWorld;

        markerRect = GetComponent<RectTransform>();

        color.a = 1f; // 알파값 1로 초기화
        GetComponent<Image>().color = color;
    }

    private void Update()
    {
        // Init() 호출 전 Update()가 실행될 수 있으므로 먼저 pool, target null 체크
        if (pool == null || target == null || minimapArea == null || markerRect == null)
        {
            return;
        }

        if (!target.gameObject.activeInHierarchy)
        {
            pool.Return(gameObject);
            Destroy(this);
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
