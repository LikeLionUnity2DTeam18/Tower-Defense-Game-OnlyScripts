using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public void OnClick()
    {
        EventManager.Trigger(new ToggleMenu());
    }
}
