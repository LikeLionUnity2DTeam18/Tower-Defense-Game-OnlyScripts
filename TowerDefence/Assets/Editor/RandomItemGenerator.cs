using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// 1) Window 메뉴에 등록
public class RandomItemGenerator : EditorWindow
{
    // --- 1) 에디터에서 설정할 값들 ---
    private ItemData targetSO;

    // Stat 프로퍼티 이름 목록 (ItemData.cs 에 선언된 필드명과 동일해야 함)
    private readonly string[] statNames = new[]
    {
        nameof(ItemData.AttackDamagePercent),
        nameof(ItemData.AttackSpeedPercent),
        nameof(ItemData.SkillPower),
        nameof(ItemData.MoveSpeed),
        nameof(ItemData.BindShotBindTime),
        nameof(ItemData.WallMaxHP),
        nameof(ItemData.PowerupDamageUpAmount),
        nameof(ItemData.FireBreathDamage),
        nameof(ItemData.FireBreathDamageInterval)
    };

    // 각 스탯별: current / min / max 값을 담아 둘 배열
    private float[] currentValues;
    private float[] minValues;
    private float[] maxValues;

    // Icon 리스트(에셋 폴더에서 읽어옵니다)
    private List<Sprite> iconList = new List<Sprite>();

    private readonly float[] defaultMinValues = new float[]
{
        0f,    // AttackDamagePercent 최소
        0f,  // AttackSpeedPercent 최소
        0f,    // SkillPower 최소
        0f,    // MoveSpeed 최소
        0f,  // BindShotBindTime 최소
        0f,   // WallMaxHP 최소
        0f,  // PowerupDamageUpAmount 최소
        0f,    // FireBreathDamage 최소
        -0.2f   // FireBreathDamageInterval 최소
};

    private readonly float[] defaultMaxValues = new float[]
    {
        50f,   // AttackDamagePercent 최대
        50f,    // AttackSpeedPercent 최대
        5f,    // SkillPower 최대
        4f,    // MoveSpeed 최대
        2f,    // BindShotBindTime 최대
        100f,   // WallMaxHP 최대
        5f,    // PowerupDamageUpAmount 최대
        5f,   // FireBreathDamage 최대
        0f     // FireBreathDamageInterval 최대
    };

    [MenuItem("Window/Random Item Generator")]
    public static void ShowWindow()
    {
        GetWindow<RandomItemGenerator>("Random Item Generator");
    }

    private void OnEnable()
    {
        // 스탯 개수만큼 배열 초기화
        int count = statNames.Length;
        currentValues = new float[count];
        minValues = new float[count];
        maxValues = new float[count];

        // 최대최소값 기본으로 채워놓기
        for(int i = 0; i < count; i++)
        {
            minValues[i] = defaultMinValues[i];
            maxValues[i] = defaultMaxValues[i];
            currentValues[i] = minValues[i];
        }


        // 아이콘을 에셋 데이터베이스에서 로드
        // (여기선 Assets/Sprites/Icons 폴더를 예시로 사용)
        iconList.Clear();

        string sheetPath = "Assets/Player/PlayerAsset/Item/#1 - Transparent Icons.png";

        Object[] assets = AssetDatabase.LoadAllAssetRepresentationsAtPath(sheetPath);

        iconList = assets.OfType<Sprite>().ToList();

    }

    private void OnGUI()
    {
        // --- Target SO 필드 ---
        EditorGUILayout.Space();
        targetSO = EditorGUILayout.ObjectField("Target ItemData", targetSO, typeof(ItemData), false) as ItemData;
        if (targetSO == null) return;

        // --- 아이콘 미리보기 + 랜덤 버튼 ---
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Icon Preview:", GUILayout.Width(80));
        if (targetSO.ItemIcon != null)
        {
            targetSO.ItemIcon = (Sprite)EditorGUILayout.ObjectField(targetSO.ItemIcon, typeof(Sprite), false,
                GUILayout.Width(50), GUILayout.Height(50));
        }
        else
            GUILayout.Box("None", GUILayout.Width(50), GUILayout.Height(50));

        if (GUILayout.Button("Randomize Icon", GUILayout.Width(120)))
        {
            Undo.RecordObject(targetSO, "Randomize Icon");
            int idx = Random.Range(0, iconList.Count);
            targetSO.ItemIcon = iconList[idx];
            EditorUtility.SetDirty(targetSO);
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();

        // --- 테이블 헤더 ---
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Stat", GUILayout.Width(120));
        GUILayout.Label("Value", GUILayout.Width(60));
        GUILayout.Label("Min", GUILayout.Width(60));
        GUILayout.Label("Max", GUILayout.Width(60));
        GUILayout.Label("Random?", GUILayout.Width(60));
        EditorGUILayout.EndHorizontal();

        // --- 각 스탯 행 ---
        for (int i = 0; i < statNames.Length; i++)
        {
            EditorGUILayout.BeginHorizontal();

            // 1) 이름
            GUILayout.Label(statNames[i], GUILayout.Width(120));

            // 2) 현재 값 (화면에만 보여주고, ApplyValue로 SO에 쓴다)
            currentValues[i] = EditorGUILayout.FloatField(currentValues[i], GUILayout.Width(60));

            // 3) 최소
            minValues[i] = EditorGUILayout.FloatField(minValues[i], GUILayout.Width(60));

            // 4) 최대
            maxValues[i] = EditorGUILayout.FloatField(maxValues[i], GUILayout.Width(60));

            // 5) 해당 행만 랜덤화
            if (GUILayout.Button("Random", GUILayout.Width(60)))
            {
                float rawValue = Random.Range(minValues[i], maxValues[i]);
                currentValues[i] = RoundToIncrement(rawValue,0.1f);
                ApplyValue(i);
            }

            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.Space();
        // --- 전체 랜덤 버튼 ---
        if (GUILayout.Button("Randomize All", GUILayout.Height(30)))
        {
            Undo.RecordObject(targetSO, "Randomize All");
            // 1) 아이콘도 같이 랜덤
            int randomIconIdx = Random.Range(0, iconList.Count);
            targetSO.ItemIcon = iconList[randomIconIdx];

            // 2) 모든 스탯 랜덤화
            for (int i = 0; i < statNames.Length; i++)
            {
                float rawValue = Random.Range(minValues[i], maxValues[i]);
                currentValues[i] = RoundToIncrement(rawValue, 0.1f);
                ApplyValue(i);
            }

            EditorUtility.SetDirty(targetSO);
        }
    }

    // 특정 인덱스(i)의 currentValues[i] 값을 targetSO의 해당 필드에 적용
    private void ApplyValue(int i)
    {
        var so = new SerializedObject(targetSO);
        var prop = so.FindProperty(statNames[i]);
        if (prop != null)
        {
            prop.floatValue = currentValues[i];
            so.ApplyModifiedProperties();
            EditorUtility.SetDirty(targetSO);
        }
    }

    /// <summary>
    /// value를 increment 단위로 반올림
    /// </summary>
    private float RoundToIncrement(float value, float increment)
    {
        return Mathf.Round(value / increment) * increment;
    }
}