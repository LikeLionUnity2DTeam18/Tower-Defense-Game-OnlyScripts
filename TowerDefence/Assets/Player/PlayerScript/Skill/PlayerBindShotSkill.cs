using System;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;


/// <summary>
/// 구속의 사격 스킬
/// </summary>
public class PlayerBindShotSkill : Skill
{
    [Header("프리펩")]
    [SerializeField] protected GameObject skillPrefabN;
    [SerializeField] protected GameObject casterPrefab;

    [Header("구속의 사격 정보")]
    [SerializeField] private float initial_damage;
    [SerializeField] private float initial_bindTime;
    [SerializeField] private float initial_castingTime;
    [SerializeField] private float skillPowerFactor; // 스킬파워 계수

    private PlayerStat damage;
    private PlayerStatModifier skillPowerMod;
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

        skillPowerMod = new PlayerStatModifier(player.SkillPower * skillPowerFactor,PlayerStatModifierMode.additive);
        damage.AddModifier(skillPowerMod);

        player.Stats.SkillPower.OnValueChanged += OnSkillPowerChanged;
    }

    private void OnSkillPowerChanged()
    {
        skillPowerMod.SetValue(player.SkillPower * skillPowerFactor);
    }

    private void OnDestroy()
    {
        player.Stats.SkillPower.OnValueChanged -= OnSkillPowerChanged;
    }

    protected override void UseSkill()
    {
        base.UseSkill();

        CreateCasterSkillEffect();
        player.StateMachine.ChangeState(player.BindShotState); // 시전시간동안 멈추고 조작x
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
        SoundManager.Instance.Play(SoundType.BindShotCast, player.transform);
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
        SoundManager.Instance.Play(SoundType.BindShotLand, player.transform);
    }

    /// <summary>
    /// 플레이어 스탯 타입으로 스탯 return
    /// 다음에는 skillmanager에서 관리할수있게 해야겠음..
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public override PlayerStat GetStatByType(PlayerStatTypes type)
    {
        return type switch
        {
            PlayerStatTypes.BindShotDamage => damage,
            PlayerStatTypes.BindShotBindTime => bindTime,
            PlayerStatTypes.BindShotCastingTime => castingTime,
            _ => null
        };
    }

    /// <summary>
    /// 툴팁 텍스트 get
    /// </summary>
    /// <returns></returns>
    public override string GetTooltipText()
    {
        if (tooltipText == null)
            SetTooltipText();
        return tooltipText;
    }

    /// <summary>
    /// StringBuilder를 이용해 툴팁 문자열 생성
    /// </summary>
    public override void SetTooltipText()
    {

        var sb = new StringBuilder();

        // 스킬 이름
        sb.AppendLine($"<b>구속의 사격</b>");
        sb.AppendLine($"쿨타임: {cooldown}초");
        sb.AppendLine(); // 빈 줄
        // 스킬 스탯
        sb.AppendLine($"데미지: {Damage}");
        sb.AppendLine($"속박 시간: {BindTime}초");
        sb.AppendLine($"시전 시간: {CastingTime}초");

        tooltipText = sb.ToString();
    }
}
