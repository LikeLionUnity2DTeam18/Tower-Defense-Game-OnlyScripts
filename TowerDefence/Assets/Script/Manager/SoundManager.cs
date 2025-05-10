using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    [Header("사운드 배열")]
    [SerializeField] public SoundData[] soundArray;

    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                // 씬에 SoundManager 오브젝트가 없으면 새로 생성
                GameObject obj = new GameObject("SoundManager");
                instance = obj.AddComponent<SoundManager>();
                DontDestroyOnLoad(obj);
            }
            return instance;
        }
    }

    void Awake()
    {
        // 중복 생성 방지
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // 초기화 작업이 있다면 여기에
    }

    void Update()
    {
        // 프레임마다 할 일 있다면 여기에
    }

    // 예시: 효과음 재생 함수
    public void Play(SoundType type, Transform pos)
    {
        int index = (int)type;

        if (index < 0 || index >= soundArray.Length)
        {
            Debug.LogWarning($"[SoundManager] soundArray[{index}] 범위 오류");
            return;
        }

        AudioClip clip = soundArray[index].clip;

        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, pos.position);
        }
        else
        {
            Debug.LogWarning($"[SoundManager] SoundType.{type} 에 대응하는 오디오 클립이 없습니다.");
        }
    }
}


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
    Click,
    TowerSummon,
}