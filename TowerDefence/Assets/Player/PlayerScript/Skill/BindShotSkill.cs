using UnityEngine;

public class BindShotSkill : Skill
{
    [Header("프리펩")]
    [SerializeField] protected GameObject skillPrefabN;

    [Header("구속의 사격 정보")]
    [SerializeField] private float damage;
    [SerializeField] private float bindTime;

    protected override void Start()
    {
        base.Start();
        canBeFlipX = false;
    }

    protected override void UseSkill()
    {
        base.UseSkill();

        GameObject go;
        if (previewDirection == Direction4Custom.NE || previewDirection == Direction4Custom.NW)
            go = PoolManager.Instance.Get(skillPrefabN);
        else
            go = PoolManager.Instance.Get(skillPrefab);

        go.GetComponent<BindShotController>().SetBindShot(mousePos, damage, bindTime, previewDirection);
    }
}
