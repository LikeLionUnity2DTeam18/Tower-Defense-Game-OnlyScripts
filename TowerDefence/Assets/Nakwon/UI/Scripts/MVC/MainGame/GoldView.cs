using UnityEngine;
using TMPro;
public class GoldView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldText;

    public void OnGoldChanged(GoldChanged evt)
    {
        goldText.text = evt.NewGold.ToString();
    }
}
