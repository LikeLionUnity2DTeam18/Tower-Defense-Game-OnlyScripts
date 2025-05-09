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

    public void Show(string content, Vector2 mousePosition)
{
    if (pannel.activeSelf) return;

    pannel.SetActive(true);
    text.text = content;

    Vector2 offset = new Vector2(0, 25f);

    // 스크린 좌표(mousePosition)를 Canvas 내부 로컬 좌표로 변환
    RectTransformUtility.ScreenPointToLocalPointInRectangle(
        transform as RectTransform,
        mousePosition + offset,
        null, // 👉 만약 Screen Space - Camera라면 여기에 Camera 넣어야 함
        out Vector2 localPoint
    );

    // 로컬 좌표로 적용
    (pannel.transform as RectTransform).anchoredPosition = localPoint;
}

    public void Hide()
    {
        pannel.SetActive(false);
    }
}
