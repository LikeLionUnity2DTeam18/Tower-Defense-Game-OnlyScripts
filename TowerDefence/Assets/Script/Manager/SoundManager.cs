using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    [SerializeField] private AudioSource bgmSource;


    [Header("사운드 데이터 배열")]
    [SerializeField] private SoundData[] soundArray;
    [Header("오디오소스 풀용 프리팹")]
    [SerializeField] private GameObject audioSourcePrefab;

    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("SoundManager");
                instance = obj.AddComponent<SoundManager>();
                DontDestroyOnLoad(obj);
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        PoolManager.Instance.CreatePool(audioSourcePrefab, 10);
    }

    private void Start()
    {
        EventManager.AddListener<StageChangeEvent>(OnStageChange);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<StageChangeEvent>(OnStageChange);
    }

    private void OnStageChange(StageChangeEvent evt)
    {
        switch (evt.EventType)
        {
            case StageChangeEventType.Start:
                Play(SoundType.StageStart, transform);
                bgmSource.DOPitch(0.8f, 1f);
                break;
            case StageChangeEventType.End:
                Play(SoundType.StageEnd, transform);
                bgmSource.DOPitch(1f, 1f);
                break;
        }
    }

    public void Play(SoundType type, Transform pos)
    {
        int index = (int)type;

        if (index < 0 || index >= soundArray.Length)
        {
            Debug.LogWarning($"[SoundManager] soundArray[{index}] 범위 오류");
            return;
        }

        AudioClip clip = soundArray[index].clip;

        if (clip == null)
        {
            Debug.LogWarning($"[SoundManager] SoundType.{type} 에 대응하는 오디오 클립이 없습니다.");
            return;
        }

        GameObject obj = PoolManager.Instance.Get(audioSourcePrefab);
        SoundPlayer player = obj.GetComponent<SoundPlayer>();
        player.Play(clip, pos.position);
    }
}


//[CreateAssetMenu(fileName = "SoundData", menuName = "Sound/SoundData")]
//public class SoundData : ScriptableObject
//{
//    public SoundType type;
//    public AudioClip clip;
//}

//public enum SoundType
//{
//    None,
//    Explosion,
//    StageStart,
//    StageEnd,
//    Money,
//    Gacha,
//    Hit,
//    EnemyHit,
//    Click,
//    TowerSummon,
//}