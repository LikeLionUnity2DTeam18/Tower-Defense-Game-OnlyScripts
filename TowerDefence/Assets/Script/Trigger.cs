using UnityEngine;

public class Trigger : MonoBehaviour
{
    move mv;

    private void Awake()
    {
        mv = GetComponentInParent<move>();
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
