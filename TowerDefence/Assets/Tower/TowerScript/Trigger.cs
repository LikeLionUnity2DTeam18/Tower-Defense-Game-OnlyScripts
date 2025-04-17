using UnityEngine;

public class Trigger : MonoBehaviour
{
    TowerMove mv;

    private void Awake()
    {
        mv = GetComponentInParent<TowerMove>();
    }


    public void TriggerMove()
    {
        mv.Move();
    }

    public void TriggerStop()
    {
        mv.Stop();
    }

    public void TriggerFlip()
    {
        mv.Flip();
    }
}
