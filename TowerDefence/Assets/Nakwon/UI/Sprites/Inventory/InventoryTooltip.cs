using TMPro;
using UnityEngine;

public class InventoryTooltip : MonoBehaviour
{
    [SerializeField] private GameObject thisObj;
    [SerializeField] private TextMeshProUGUI tmp;

    public void ShowTooltip(Vector2 pos, string text)
    {
        thisObj.SetActive(true);
        transform.position = pos;
        tmp.text = text;
    }

    public void HideTooltip()
    {
        thisObj.SetActive(false);
    }
}
