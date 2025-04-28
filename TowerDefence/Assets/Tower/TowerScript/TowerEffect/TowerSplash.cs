using UnityEngine;

public class TowerSplash : TowerEntity
{
    //애니메이션 끝나면 삭제
    public void AnimationTriggerEnd()
    {
        PoolManager.Instance.Return(gameObject);
    }
}
