using UnityEditor;
using UnityEngine;

public class LevelTableGenerator : EditorWindow
{
    PlayerLevelTable targetSO;
    int maxLevel = 10;
    PlayerStatData baseStats = new();
    PlayerStatData growthValue = new();

    [MenuItem("Window/Level Table Generator")]
    static void OpenWindow()
    {
        LevelTableGenerator window = (LevelTableGenerator)GetWindow(typeof(LevelTableGenerator));
        window.minSize = new Vector2(600, 300);
        window.Show();
    }

    void OnGUI()
    {
        targetSO = (PlayerLevelTable)EditorGUILayout.ObjectField("Target SO", targetSO, typeof(PlayerLevelTable), false);
        maxLevel = EditorGUILayout.IntField("Max Level", maxLevel);
        baseStats = DrawStatDataField("Base Stats", baseStats);
        growthValue = DrawStatDataField("Growth Rates", growthValue);

        if (GUILayout.Button("Generate"))
        {
            if (targetSO == null) return;

            Undo.RecordObject(targetSO, "Generate Level Table");

            targetSO.table.Clear();
            targetSO.table.Add(null);
            for (int i = 1; i <= maxLevel ; i++)
                targetSO.table.Add(ComputeData(i, baseStats, growthValue));

            EditorUtility.SetDirty(targetSO);
        }
    }

    PlayerStatData DrawStatDataField(string label, PlayerStatData data)
    {
        EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
        // 예: data.hp, data.attack … 각 필드
        data.baseAttackDamage = EditorGUILayout.FloatField("BaseAttackDamage", data.baseAttackDamage);
        data.baseattackSpeed = EditorGUILayout.FloatField("baseattackSpeed", data.baseattackSpeed);
        data.baseattackRange = EditorGUILayout.FloatField("baseattackRange", data.baseattackRange);
        data.skillPower = EditorGUILayout.FloatField("skillPower", data.skillPower);
        data.moveSpeed = EditorGUILayout.FloatField("moveSpeed", data.moveSpeed);

        return data;
    }

    PlayerStatData ComputeData(int lvl, PlayerStatData baseD, PlayerStatData growth)
    {
        var newData = new PlayerStatData();
        newData.baseAttackDamage = baseD.baseAttackDamage + growth.baseAttackDamage * (lvl-1);
        newData.baseattackSpeed = baseD.baseattackSpeed + growth.baseattackSpeed * (lvl-1);
        newData.baseattackRange = baseD.baseattackRange + growth.baseattackRange * (lvl-1);
        newData.skillPower = baseD.skillPower + growth.skillPower * (lvl-1);
        newData.moveSpeed = baseD.moveSpeed + growth.moveSpeed * (lvl-1);

        return newData;
    }
}
