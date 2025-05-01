using UnityEngine;
using UnityEngine.UIElements;

public enum BindShotStatTypes { Damage, BindTime, CastingTime}

public class PlayerBindShotSkill : Skill
{
    [Header("프리펩")]
    [SerializeField] protected GameObject skillPrefabN;
    [SerializeField] protected GameObject casterPrefab;

    [Header("구속의 사격 정보")]
    [SerializeField] private float initial_damage;
    [SerializeField] private float initial_bindTime;
    [SerializeField] private float initial_castingTime;

    private PlayerStat damage;
    private PlayerStat bindTime;
    private PlayerStat castingTime;

    public float Damage => damage.GetValue();
    public float BindTime => bindTime.GetValue();
    public float CastingTime => castingTime.GetValue();

    protected override void Start()
    {
        base.Start();
        canBeFlipX = false;
        InitilizeStats();
    }

    private void InitilizeStats()
    {
        damage = new PlayerStat(initial_damage);
        bindTime = new PlayerStat(initial_bindTime);
        castingTime = new PlayerStat(initial_castingTime);
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

        go.GetComponent<BindShotController>().SetBindShot(skillCenterPosition, Damage, BindTime, previewDirection);
    }

    public void AddModifier(BindShotStatTypes type, PlayerStatModifier modifier)
    {
        PlayerStat targetStat = GetStatType(type);

        targetStat?.AddModifier(modifier);
    }


    public void RemoveModifier(BindShotStatTypes type, PlayerStatModifier modifier)
    {
        PlayerStat targetStat = GetStatType(type);

        targetStat?.RemoveModifier(modifier);
    }
    private PlayerStat GetStatType(BindShotStatTypes type)
    {
        return type switch
        {
            BindShotStatTypes.Damage => damage,
            BindShotStatTypes.BindTime => bindTime,
            BindShotStatTypes.CastingTime => castingTime,
            _ => null
        };
    }
}
