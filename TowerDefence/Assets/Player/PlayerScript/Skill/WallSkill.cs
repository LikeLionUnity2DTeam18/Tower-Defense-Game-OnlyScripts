using UnityEngine;

public class WallSkill : Skill
{
    [Header("벽 정보")]
    [SerializeField] private float HP;
    [SerializeField] private float duration;



    public override bool TryPreviewSkill()
    {
        return base.TryPreviewSkill();
    }

    protected override void Start()
    {
        base.Start();
        canBeFlipX = true;
    }

    protected override void UseSkill()
    {
        base.UseSkill();
        Debug.Log("벽스킬");
        var go = PoolManager.Instance.Get(skillPrefab);
        var wallScript = go.GetComponent<WallSkillController>();
        wallScript.SetWall(mousePos, HP, duration, isDirectionSE);
    }
}
