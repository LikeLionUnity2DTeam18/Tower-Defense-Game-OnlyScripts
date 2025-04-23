using UnityEngine;

public class EyeDraw : MonoBehaviour
{
    Animator anim;
    [SerializeField] private GameObject[] icons;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnMouseEnter()
    {
        anim.SetBool("Enter",true);
    }
    private void OnMouseDown()
    {
        anim.SetBool("Click", true);
        DrawTowerIcon();
    }
    private void OnMouseExit() 
    {
        anim.SetBool("Enter", false);
    }

    private void AnimTrigger()
    {
        anim.SetBool("Click", false);
    }

    private void DrawTowerIcon()
    {
        GameObject t = PoolManager.Instance.Get(icons[0]);
    }
}
