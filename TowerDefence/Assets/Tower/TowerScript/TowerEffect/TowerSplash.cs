using UnityEngine;

public class TowerSplash : MonoBehaviour
{
    public void AnimationTriggerEnd()
    {
        PoolManager.Instance.Return(gameObject);
    }
}
