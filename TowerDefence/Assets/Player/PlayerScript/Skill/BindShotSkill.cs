using UnityEngine;

public class BindShotSkill : Skill
{
    [Header("프리펩")]
    [SerializeField] protected GameObject skillPrefabN;
    [SerializeField] protected GameObject casterPrefab;

    [Header("구속의 사격 정보")]
    [SerializeField] private float damage;
    [SerializeField] private float bindTime;
    [SerializeField] private float castingTime;
    public float CastingTime => castingTime;

    protected override void Start()
    {
        base.Start();
        canBeFlipX = false;
    }

    protected override void UseSkill()
    {
        base.UseSkill();

        CreateCasterSkillEffect();
        player.stateMachine.ChangeState(player.bindShotState);
    }

    private void CreateCasterSkillEffect()
    {
        Vector3 effectPosition = player.transform.position + new Vector3(0.75f, 2f, 0);
        bool isEast = previewDirection == Direction4Custom.SE || previewDirection == Direction4Custom.NE;
        GameObject go = PoolManager.Instance.Get(casterPrefab);
        go.GetComponent<BindShotCasterEffectController>().SetEffect(effectPosition, isEast, this);
    }

    public void CreateSkillObject()
    {
        GameObject go;
        if (previewDirection == Direction4Custom.NE || previewDirection == Direction4Custom.NW)
            go = PoolManager.Instance.Get(skillPrefabN);
        else
            go = PoolManager.Instance.Get(skillPrefab);

        go.GetComponent<BindShotController>().SetBindShot(skillCenterPosition, damage, bindTime, previewDirection);
    }
}
