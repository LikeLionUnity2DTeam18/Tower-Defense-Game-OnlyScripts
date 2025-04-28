using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    public PlayerController Player => player;
    public static PlayerManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("PlayerManager");
                instance = obj.AddComponent<PlayerManager>();
                DontDestroyOnLoad(obj);
            }
            return instance;
        }
    }
    private static PlayerManager instance;


    private void Awake()
    {
        if(player == null)
        {
            if (player == null)
            {
                player = FindAnyObjectByType<PlayerController>();
                if (player == null)
                {
                    Debug.LogError("PlayerManager: PlayerController를 찾을 수 없습니다!");
                }
            }
        }
    }
}
