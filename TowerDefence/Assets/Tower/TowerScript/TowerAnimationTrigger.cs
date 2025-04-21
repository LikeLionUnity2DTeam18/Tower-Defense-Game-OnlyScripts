using UnityEngine;

public class TowerAnimationTrigger : MonoBehaviour
{
    private Tower tower => GetComponent<Tower>();


    public void AnimationTrigger1()
    {
        tower.AnimationTrigger();
        Debug.Log("애니메이션 끝");
    }
}
