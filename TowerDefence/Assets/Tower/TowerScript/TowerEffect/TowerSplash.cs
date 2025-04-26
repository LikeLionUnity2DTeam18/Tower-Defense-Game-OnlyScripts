using UnityEngine;

public class TowerSplash : MonoBehaviour
{
    //애니메이션 끝나면 삭제
    public void AnimationTriggerEnd()
    {
        PoolManager.Instance.Return(gameObject);
    }
}
