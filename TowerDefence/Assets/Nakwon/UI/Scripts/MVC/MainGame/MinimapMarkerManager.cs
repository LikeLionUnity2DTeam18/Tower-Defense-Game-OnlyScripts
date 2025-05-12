using UnityEngine;
using System.Collections.Generic;
public class MinimapMarkerManager : MonoBehaviour
{
    [SerializeField] private GameObject markerPrefab;
    [SerializeField] private RectTransform markerContainer;
    [SerializeField] private RectTransform minimapArea;
    [SerializeField] private float mapSizeWorld = 100f;

    [Header("Marker Colors")]
    [SerializeField] private Color playerColor;
    [SerializeField] private Color TowerColor;
    [SerializeField] private Color enemyColor;

    private ObjectPool markerPool;

    private void Awake()
    {
        markerPool = new ObjectPool(markerPrefab, 20, markerContainer);
    }

    private void Start()
    {
        //Player 자동 등록
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            RegisterTarget(player.transform, MinimapMarkerType.Player);
        }

        //Enemy 자동 등록
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            RegisterTarget(enemy.transform, MinimapMarkerType.Enemy);
        }
        //Tower 자동 등록
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
        foreach (GameObject tower in towers)
        {
            RegisterTarget(tower.transform, MinimapMarkerType.Tower);
        }
    }

    public void RegisterTarget(Transform target, MinimapMarkerType type)
    {
        // GameObject markerObj = markerPool.Get();
        // markerObj.transform.SetParent(markerContainer, false);
        // var marker = markerObj.GetComponent<MinimapMarkerFollow>();
        // Color color = GetColorForType(type);
        // marker.Init(target, color, markerPool, minimapArea, mapSizeWorld);
        if (target == null || target.gameObject == null)
        {
            Debug.LogWarning("RegisterTarget 실패: target이 null이거나 이미 파괴됨.");
            return;
        }

        GameObject markerObj = markerPool.Get();

        if (markerObj == null)
        {
            Debug.LogError("markerObj가 풀에서 null로 반환됨");
            return;
        }

        markerObj.transform.SetParent(markerContainer, false);

        var marker = markerObj.GetComponent<MinimapMarkerFollow>();
        if (marker == null)
        {
            Debug.LogError($"MinimapMarkerFollow가 {markerObj.name}에 없습니다.");
            return;
        }

        Color color = GetColorForType(type);
        marker.Init(target, color, markerPool, minimapArea, mapSizeWorld);
        if (minimapArea == null) Debug.Log("#$%@#$%@#$%@#$%null 찾았다");
    }

    private Color GetColorForType(MinimapMarkerType type)
    {
        return type switch
        {
            MinimapMarkerType.Player => playerColor,
            MinimapMarkerType.Tower => TowerColor,
            MinimapMarkerType.Enemy => enemyColor,
            _ => Color.white
        };
    }

    private void OnEnable()
    {
        EventManager.AddListener<EnemySpawned>(OnEnemySpawned);
        EventManager.AddListener<TowerSpawned>(OnTowerSpawned);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<EnemySpawned>(OnEnemySpawned);
        EventManager.RemoveListener<TowerSpawned>(OnTowerSpawned);
    }

    private void OnEnemySpawned(EnemySpawned evt)
    {
        RegisterTarget(evt.enemyTransform, MinimapMarkerType.Enemy);
    }

    private void OnTowerSpawned(TowerSpawned evt)
    {
        RegisterTarget(evt.towerTransform, MinimapMarkerType.Tower);
    }
}
