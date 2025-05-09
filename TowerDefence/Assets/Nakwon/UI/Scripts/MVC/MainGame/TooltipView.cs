using UnityEngine;
using TMPro;

public class TooltipView : MonoBehaviour
{
    [SerializeField] private GameObject pannel;
    [SerializeField] private TextMeshProUGUI text;
    void Awake()
    {
        Hide();
    }

    public void Show(string content, RectTransform targetIcon)
    {
        if (pannel.activeSelf) return;

        pannel.SetActive(true);
        text.text = content;

        // 기준 아이콘의 위쪽에 툴팁 위치 고정
        Vector2 offset = new Vector2(150f, 150f); // Y축 위로 50 정도 띄움
        Vector3 worldPos = targetIcon.position;
        pannel.transform.position = worldPos + (Vector3)offset;
    }

    public void Hide()
    {
        pannel.SetActive(false);
    }
}
