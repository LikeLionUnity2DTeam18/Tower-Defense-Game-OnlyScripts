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

    /// <summary>
    /// 미리보기 상태에서 키 한번 더 입력 시 스킬 시전, 시전자 머리위에 시전동작 프리펩 생성
    /// 시전동작 애니메이션 이벤트에서 CreateSkillObject() 호출
    /// </summary>
    private void CreateCasterSkillEffect()
    {
        Vector3 effectPosition = player.transform.position + new Vector3(0.3f, 2f, 0);
        bool isEast = previewDirection == Direction4Custom.SE || previewDirection == Direction4Custom.NE;
        GameObject go = PoolManager.Instance.Get(casterPrefab);
        go.GetComponent<BindShotCasterEffectController>().SetEffect(effectPosition, isEast, this);
    }

    /// <summary>
    /// 시전동작 애니매이션 이벤트로 호출돼서 스킬사용지점에 스킬효과 생성
    /// </summary>
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
