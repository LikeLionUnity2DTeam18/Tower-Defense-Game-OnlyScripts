using UnityEngine;

[CreateAssetMenu(fileName = "SoundData", menuName = "Sound/SoundData")]
public class SoundData : ScriptableObject
{
    public SoundType type;
    public AudioClip clip;
}

public enum SoundType
{
    None,
    Explosion,
    StageStart,
    StageEnd,
    Money,
    Gacha,
    Hit,
    EnemyHit,
    Click,
    TowerSummon,
}