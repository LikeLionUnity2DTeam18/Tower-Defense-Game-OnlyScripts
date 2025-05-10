using UnityEngine;

public class MenuSubButtons : MonoBehaviour
{
    [SerializeField] private MenuButtonTypes type;


    public void OnClick()
    {
        EventManager.Trigger(new MenuButtonClicked(type));
    }
}
