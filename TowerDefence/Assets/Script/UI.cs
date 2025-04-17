using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private float fadeSpeed = 1f;
    private bool alphaChange = false;
    private float targetAlpha;
    private Image _image;

    void Start()
    {
        _image = GetComponent<Image>();
    }

    // ��ư ��� ȣ��
    public void OnButtonClick()
    {
        alphaChange = true;
        targetAlpha = (_image.color.a == 0f) ? 1f : 0f;
    }

    public void DragEvent(BaseEventData data)
    {
        OnDrag((PointerEventData)data);
    }
    public void OnDrag(PointerEventData eventData)
    {
        // ���콺 ��ġ�� �̹��� �̵�
        transform.position = eventData.position;
    }


    void Update()
    {
        if (!alphaChange) return;

        Color c = _image.color;
        c.a = Mathf.MoveTowards(c.a, targetAlpha, Time.deltaTime * fadeSpeed);
        _image.color = c;

        if (Mathf.Approximately(c.a, targetAlpha))
            alphaChange = false;
    }


}
