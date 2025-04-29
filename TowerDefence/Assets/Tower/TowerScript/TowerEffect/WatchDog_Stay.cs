using DG.Tweening;
using UnityEngine;

public class WatchDog_Splash : TowerStay
{
    Renderer rend;
    Material mat;
    private void Awake()
    {
        rend = GetComponent<Renderer>();
        mat = rend.material;
    }
    private void Start()
    {
        mat.DOFloat(0f,"_Cutoff", 0.1f).SetTarget(this);
    }
    protected override void DurationEnd()
    {
        if (timer <= 0)
        {
            mat.DOFloat(0.5f, "_Cutoff", 0.5f).SetTarget(this).OnComplete(ResetProjectile);
        }
    }
}
