using System.Collections;
using UnityEngine;

public class TowerProjectile : MonoBehaviour
{



    public void AnimationTriggerEnd()
    {
        PoolManager.Instance.Return(gameObject);
    }
}
